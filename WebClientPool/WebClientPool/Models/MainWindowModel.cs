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
    public class MainWindowModel : BindableBase
    {
        #region Fields
        
        private string _logText = string.Empty;

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
        }

        public async Task Download(string urlText)
        {
            using var webClientPool = new WebClientPool<DummyWebClient>(3, client =>
            {
                client.DownloadStarted.Subscribe(DownloadStarted);
                client.CompletedChanged.Subscribe(DownloadCompleted);
            });

            var convertedUrlText = urlText.Replace("\r\n", "\n").Replace("\r", "\n");
            var urls = from x in convertedUrlText.Split('\n') where !string.IsNullOrEmpty(x) select x;

            await DownloadTask(urls, webClientPool);

            LogText += "Finished All.";
        }

        private static async Task DownloadTask(IEnumerable<string> urls, WebClientPool<DummyWebClient> webClientPool)
        {
            var tasks = new List<Task>();
            foreach (var (url, index) in urls.Select((v, i) => (Value: v, Index: i)))
            {
                var webClientInfo = await webClientPool.GetWebClient();
                var task = Task.Factory.StartNew(() =>
                {
                    webClientInfo.Client.DownloadString(url, index);
                    webClientPool.ReturnWebClient(webClientInfo);
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public void DownloadStarted(string url)
        {
            lock (LogText)
            {
                LogText += $"Start {url}\n";
            }
        }

        public void DownloadCompleted(string url)
        {
            lock (LogText)
            {
                LogText += $"Finished {url}\n";
            }
        }
    }
}
