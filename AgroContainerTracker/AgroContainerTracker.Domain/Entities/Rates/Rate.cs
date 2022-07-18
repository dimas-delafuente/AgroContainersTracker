namespace AgroContainerTracker.Domain
{
    public class Rate : Entity<int>, IAggregate
    {
        public string Name { get; set; } = null!;

        public Price MainPrice { get; set; } = null!;

        public Price SecondaryPrice { get; set; } = null!;

        public string? Description { get; set; }
    }
}
