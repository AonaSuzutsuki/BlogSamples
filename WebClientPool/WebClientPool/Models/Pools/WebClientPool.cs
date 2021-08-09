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
    public class WebClientPool<T> : IDisposable where T : IDisposable, new()
    {
        private readonly ImmutableList<WebClientInfo<T>> _clients;

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

        public async Task<IWebClientInfo<T>> GetWebClient()
        {
            return await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    foreach (var client in _clients.Where(client => !client.IsBusy))
                    {
                        client.IsBusy = true;
                        return client;
                    }

                    Thread.Sleep(100);
                }
            });
        }

        public void ReturnWebClient(IWebClientInfo<T> webClient)
        {
            var client = _clients[webClient.Id];
            client.IsBusy = false;
        }

        public void Dispose()
        {
            foreach (var client in _clients)
            {
                client.Dispose();
            }
        }
    }
}