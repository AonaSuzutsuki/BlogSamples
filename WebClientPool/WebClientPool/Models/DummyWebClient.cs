using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

namespace WebClientPool.Models
{
    public class DummyWebClient : IDisposable
    {
        #region Events

        private readonly Subject<string> _downloadStartedSubject = new();
        private readonly Subject<string> _completedSubject = new();

        public IObservable<string> DownloadStarted => _downloadStartedSubject;
        public IObservable<string> CompletedChanged => _completedSubject;

        #endregion

        private object _lockObj = new object();
        private int _seed = 1;

        public void DownloadString(string url)
        {
            //var random = new Random(_seed++);

            //_downloadStartedSubject.OnNext(url);
            //Thread.Sleep(random.Next(0, 5000));
            //_completedSubject.OnNext(url);

            var cnt = new[] { 3000, 500 };
            var seed = 0;
            lock (_lockObj)
            {
                seed = _seed++;
            }
            _downloadStartedSubject.OnNext(url);
            Thread.Sleep(cnt[seed % 2]);
            _completedSubject.OnNext(url);
        }

        public void Dispose()
        {
            _downloadStartedSubject?.Dispose();
            _completedSubject?.Dispose();
        }
    }
}
