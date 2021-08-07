using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using WebClientPool.Models.Pools;

namespace WebClientPool.Models
{
    public class MainWindowModel : BindableBase, IDisposable
    {
        #region Fields
        
        private string _logText = string.Empty;

        private readonly WebClientPool<DummyWebClient> _webClientPool;

        #endregion

        #region Properties
        
        public string LogText
        {
            get => _logText;
            set => SetProperty(ref _logText, value);
        }

        #endregion

        public MainWindowModel()
        {
            _webClientPool = new WebClientPool<DummyWebClient>(3, client =>
            {
                client.DownloadStarted.Subscribe(DownloadStarted);
                client.CompletedChanged.Subscribe(DownloadCompleted);
            });
        }

        public async Task Download(string urlText)
        {
            var convertedUrlText = urlText.Replace("\r\n", "\n").Replace("\r", "\n");
            var urls = from x in convertedUrlText.Split('\n') where !string.IsNullOrEmpty(x) select x;
            var tasks = new List<Task>();
            foreach (var url in urls)
            {
                var webClient = await _webClientPool.GetWebClient();
                var task = Task.Factory.StartNew(() =>
                {
                    webClient.Client.DownloadString(url);
                    _webClientPool.ReturnWebClient(webClient);
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public void DownloadStarted(string url)
        {
            Debug.WriteLine($"Start {url}");
        }

        public void DownloadCompleted(string url)
        {
            Debug.WriteLine($"Finished {url}");
        }

        public void Dispose()
        {
            _webClientPool?.Dispose();
        }
    }
}
