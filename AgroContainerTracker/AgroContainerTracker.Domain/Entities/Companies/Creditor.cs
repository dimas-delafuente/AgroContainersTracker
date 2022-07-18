namespace AgroContainerTracker.Domain
{
    public class Creditor : IAggregate
    {
        public Company Company { get; set; } = null!;
    }
}
