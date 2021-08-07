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
        
        private int _seed = 1;

        public void DownloadString(string url, int num)
        {
            //var random = new Random(_seed++);

            //_downloadStartedSubject.OnNext(url);
            //Thread.Sleep(random.Next(0, 5000));
            //_completedSubject.OnNext(url);

            var cnt = new[] { 5000, 500 };
            _downloadStartedSubject.OnNext(url);
            Thread.Sleep(cnt[num % 2]);
            _completedSubject.OnNext(url);
        }

        public void Dispose()
        {
            _downloadStartedSubject?.Dispose();
            _completedSubject?.Dispose();
        }
    }
}
