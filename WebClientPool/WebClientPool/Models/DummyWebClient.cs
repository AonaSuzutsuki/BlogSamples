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
        
        public void DownloadString(string url, int num)
        {
            var cnt = new[] { 3000, 500 };
            _downloadStartedSubject.OnNext(url);
            Thread.Sleep(cnt[num % 2]);

            if (num % 5 == 0)
                throw new Exception();

            _completedSubject.OnNext(url);
        }

        public void Dispose()
        {
            _downloadStartedSubject?.Dispose();
            _completedSubject?.Dispose();
        }
    }
}
