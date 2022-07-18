namespace AgroContainerTracker.Domain
{
    public class Driver : Entity<int>, IAggregate
    {
        public string IdentificationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Carrier Carrier { get; set; } = null!;
    }
}