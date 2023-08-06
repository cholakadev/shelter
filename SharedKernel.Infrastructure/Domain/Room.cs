namespace SharedKernel.Infrastructure.Domain
{
    public class Room
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public RoomView View { get; set; }

        /// <summary>Amount of rooms with the same view, capacity and price.</summary>
        public int Count { get; set; }

        public IEnumerable<PriceSetting> PriceSettings { get; set; }
    }
}
