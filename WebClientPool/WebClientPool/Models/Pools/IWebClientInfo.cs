namespace WebClientPool.Models.Pools
{
    /// <summary>
    /// Provides an interface to access pooled WebClient objects.
    /// </summary>
    /// <typeparam name="T">WebClient.</typeparam>
    public interface IWebClientInfo<out T>
    {
        /// <summary>
        /// Internal ID to use in the pool.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// A pooled WebClient objects.
        /// </summary>
        T Client { get; }
    }
}