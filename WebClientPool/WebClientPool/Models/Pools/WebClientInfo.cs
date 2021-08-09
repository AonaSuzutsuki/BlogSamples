using System;
using System.Threading;

namespace WebClientPool.Models.Pools
{
    public class WebClientInfo<T> : IWebClientInfo<T>, IDisposable where T : IDisposable
    {
        #region Fields

        private readonly ReaderWriterLockSlim _readerWriterLock = new ();
        private bool _isBusy;

        #endregion

        #region Properties

        public int Id { get; }
        public T Client { get; }

        public bool IsBusy
        {
            get
            {
                try
                {
                    _readerWriterLock.EnterReadLock();
                    return _isBusy;
                }
                finally
                {
                    _readerWriterLock.ExitReadLock();
                }
            }
            set
            {
                try
                {
                    _readerWriterLock.EnterWriteLock();
                    _isBusy = value;
                }
                finally
                {
                    _readerWriterLock.ExitWriteLock();
                }
            }
        }

        #endregion

        public WebClientInfo(int id, T client)
        {
            Id = id;
            Client = client;
        }

        public override string ToString()
        {
            return $"Id: {Id}, IsBusy: {IsBusy}";
        }

        public void Dispose()
        {
            _readerWriterLock?.Dispose();
            Client?.Dispose();
        }
    }
}