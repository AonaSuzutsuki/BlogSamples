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

        private bool _isDispose = false;
        private readonly Subject<string> _downloadStartedSubject = new();
        private readonly Subject<string> _completedSubject = new();

        public IObservable<string> DownloadStarted => _downloadStartedSubject;
        public IObservable<string> CompletedChanged => _completedSubject;

        #endregion
        
        public void DownloadString(string url, int num)
        {
            //var random = new Random(_seed++);

            //_downloadStartedSubject.OnNext(url);
            //Thread.Sleep(random.Next(0, 5000));
            //_completedSubject.OnNext(url);

            var cnt = new[] { 3000, 500 };
            _downloadStartedSubject.OnNext(url);
            Thread.Sleep(cnt[num % 2]);
            _completedSubject.OnNext(url);
        }

        public void Dispose()
        {
            if (_isDispose)
                throw new ObjectDisposedException("DummyWebClient");

            _downloadStartedSubject?.Dispose();
            _completedSubject?.Dispose();
            _isDispose = true;
        }
    }
}
