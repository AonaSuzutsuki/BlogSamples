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
    /// Provides a pooled WebClient.
    /// </summary>
    /// <typeparam name="T">WebClient.</typeparam>
    public class WebClientPool<T> : IDisposable where T : IDisposable, new()
    {
        private readonly ImmutableList<WebClientInfo<T>> _clients;

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="size">Max number of pooled WebClient.</param>
        /// <param name="postProcessing">Action to be performed after the WebClient instance has been created.</param>
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
        /// Get the WebClient that is available.
        /// </summary>
        /// <returns>The WebClient that is available.</returns>
        public async Task<IWebClientInfo<T>> GetWebClient()
        {
            return await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var client = _clients.FirstOrDefault(x => !x.IsBusy);
                    if (client != null)
                    {
                        client.IsBusy = true;
                        return client;
                    }

                    Thread.Sleep(100);
                }
            });
        }

        /// <summary>
        /// Return the WebClient used.
        /// </summary>
        /// <param name="webClient">The used WebClient.</param>
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