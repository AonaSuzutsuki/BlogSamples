using System;
using System.Threading.Tasks;

namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// プールされたWebClientにアクセスするインタフェースです。
    /// </summary>
    /// <typeparam name="T">WebClientなど</typeparam>
    public interface IWebClientInfo<out T>
    {
        /// <summary>
        /// プールオブジェクトで使用する内部ID
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 非同期タスクを実行します。
        /// </summary>
        /// <param name="callback">実行するコールバック</param>
        /// <returns>非同期タスクオブジェクト</returns>
        Task StartTask(Action<T> callback);
    }
}