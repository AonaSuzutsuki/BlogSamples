using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// WebClientの使用状況を管理します。
    /// </summary>
    /// <typeparam name="T">WebClientなど</typeparam>
    public class WebClientPool<T> : IDisposable where T : IDisposable, new()
    {
        private readonly ImmutableList<WebClientInfo<T>> _clients;

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="size">プールするWebClientの最大数</param>
        /// <param name="postProcessing">WebClientのインスタンスが生成された後に処理するActionオブジェクト</param>
        public WebClientPool(int size, Action<T> postProcessing)
        {
            var clients = new List<WebClientInfo<T>>();
            foreach (var i in Enumerable.Range(0, size))
            {
                var client = new T();
                postProcessing?.Invoke(client);
                clients.Add(new WebClientInfo<T>(i, client));
            }

            _clients = ImmutableList.Create(clients.ToArray());
        }

        /// <summary>
        /// 使用可能なWebClientを返します。
        /// </summary>
        /// <returns>使用可能なWebClientオブジェクト</returns>
        public async Task<IWebClientInfo<T>> GetWebClient()
        {
            return await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    foreach (var webClientInfo in _clients)
                    {
                        lock (webClientInfo)
                        {
                            if (webClientInfo.IsBusy)
                                continue;

                            webClientInfo.IsBusy = true;
                            return webClientInfo;
                        }
                    }

                    Thread.Sleep(100);
                }
            });
        }

        /// <summary>
        /// 使用したWebClientを返却します。
        /// </summary>
        /// <param name="webClient">使用したWebClientオブジェクト</param>
        public void ReturnWebClient(IWebClientInfo<T> webClient)
        {
            var client = _clients[webClient.Id];
            lock (client)
            {
                client.IsBusy = false;
            }
        }

        public void Dispose()
        {
            foreach (var client in _clients)
            {
                client.Client.Dispose();
            }
        }
    }
}