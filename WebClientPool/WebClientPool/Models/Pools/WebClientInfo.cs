using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// プールされたWebClientオブジェクトを管理します。
    /// </summary>
    /// <typeparam name="T">WebClientなど</typeparam>
    public class WebClientInfo<T> : IWebClientInfo<T> where T : IDisposable, new()
    {
        #region Fields

        private WebClientPool<T> _pool;

        #endregion

        #region Properties


        /// <summary>
        /// プールオブジェクトで使用する内部ID
        /// </summary>
        public int Id { get; }


        /// <summary>
        /// プールされたWebClient
        /// </summary>
        public T Client { get; }

        /// <summary>
        /// WebClientが使用中かどうかを判定します。
        /// </summary>
        public bool IsBusy { get; set; }

        #endregion

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="id">内部で使用するユニークID</param>
        /// <param name="client">プールするWebClientなどのオブジェクト</param>
        public WebClientInfo(WebClientPool<T> pool, int id, T client)
        {
            _pool = pool;
            Id = id;
            Client = client;
        }

        public Task InvokeTask(Action<T> callback)
        {
            return Task.Factory.StartNew(() =>
            {
                callback(Client);
                _pool.ReturnWebClient(this);
            });
        }

        /// <summary>
        /// 現在のオブジェクトを文字列に変換します。
        /// </summary>
        /// <returns>変換された文字列</returns>
        public override string ToString()
        {
            return $"Id: {Id}, IsBusy: {IsBusy}";
        }
    }
}