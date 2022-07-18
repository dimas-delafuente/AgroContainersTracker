namespace AgroContainerTracker.Domain
{
    public class Supplier : IAggregate
    {
        public Company Company { get; set; } = null!;
    }
}
