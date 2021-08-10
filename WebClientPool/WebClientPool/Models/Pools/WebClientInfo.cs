using System;
using System.Threading;

namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// プールされたWebClientオブジェクトを管理します。
    /// </summary>
    /// <typeparam name="T">WebClientなど</typeparam>
    public class WebClientInfo<T> : IWebClientInfo<T>
    {
        #region Properties

        public int Id { get; }
        public T Client { get; }

        /// <summary>
        /// WebClientが使用中かどうかを判定します。
        /// </summary>
        public bool IsBusy { get; set; }

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
    }
}