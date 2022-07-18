namespace AgroContainerTracker.Domain
{
    public class Input : Entity<int>, IAggregate
    {
        public Campaign Campaign { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Closed { get; set; }
        public bool HasOutput { get; set; }
        public string? Observations { get; set; }
        public bool HasHail { get; set; }
        public bool HasPlague { get; set; }
        public bool HasSecondPasses { get; set; }
        public Customer Buyer { get; set; } = null!;
        public Customer Payer { get; set; } = null!;
        public IEnumerable<InputSeller> Sellers { get; set; } = null!;
        public IEnumerable<Weighing> Weighings { get; set; } = null!;
    }
}
