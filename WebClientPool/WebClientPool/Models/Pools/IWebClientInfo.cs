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
        /// プールされたWebClient
        /// </summary>
        T Client { get; }
    }
}