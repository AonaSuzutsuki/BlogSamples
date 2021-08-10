using System;
using System.Threading;

namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// プールされたWebClientオブジェクトを管理します。
    /// </summary>
    /// <typeparam name="T">WebClientなど</typeparam>
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
        /// WebClientが使用中かどうかを判定します。
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
        /// <param name="id">内部で使用するユニークID</param>
        /// <param name="client">プールするWebClientなどのオブジェクト</param>
        public WebClientInfo(int id, T client)
        {
            Id = id;
            Client = client;
        }

        /// <summary>
        /// 現在のオブジェクトを文字列に変換します。
        /// </summary>
        /// <returns>変換された文字列</returns>
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