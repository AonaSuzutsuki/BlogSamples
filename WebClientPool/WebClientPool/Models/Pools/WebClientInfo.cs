namespace WebClientPool.Models.Pools
{
    public class WebClientInfo<T> : IWebClientInfo<T>
    {
        public int Id { get; }
        public T Client { get; }
        public bool IsBusy { get; set; }

        public WebClientInfo(int id, T client, bool isBusy)
        {
            Id = id;
            Client = client;
            IsBusy = isBusy;
        }

        public override string ToString()
        {
            return $"Id: {Id}, IsBusy: {IsBusy}";
        }
    }
}