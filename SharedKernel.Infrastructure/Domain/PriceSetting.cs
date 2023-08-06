namespace SharedKernel.Infrastructure.Domain
{
    public class PriceSetting
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
