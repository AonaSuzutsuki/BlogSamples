namespace WebClientPool.Models.Pools
{
    public interface IWebClientInfo<out T>
    {
        int Id { get; }
        T Client { get; }
    }
}