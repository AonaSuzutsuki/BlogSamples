using System;
using System.Threading;

namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// Manage the usage of pooled WebClient objects.
    /// </summary>
    /// <typeparam name="T">WebClient.</typeparam>
    public class WebClientInfo<T> : IWebClientInfo<T>, IDisposable where T : IDisposable
    {
        #region Fields

        private readonly ReaderWriterLockSlim _readerWriterLock = new ();
        private bool _isBusy;

        #endregion

        #region Properties

        public int Id { get; }
        public T Client { get; }

        /// <summary>
        /// Determines if a WebClient object is in use.
        /// </summary>
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

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="id">Unique id of number.</param>
        /// <param name="client">A pooled WebClient object.</param>
        public WebClientInfo(int id, T client)
        {
            Id = id;
            Client = client;
        }

        /// <summary>
        /// Convert to string this object.
        /// </summary>
        /// <returns>The converted to string this object.</returns>
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