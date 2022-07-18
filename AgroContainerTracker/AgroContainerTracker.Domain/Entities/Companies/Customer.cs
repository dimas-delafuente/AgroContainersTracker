namespace AgroContainerTracker.Domain
{
    public class Customer : IAggregate
    {
        public Company Company { get; private set;} = null!;
        public Rate Rate { get; private set; } = null!;
        public BillingInfo BillingInfo { get; private set; } = null!;
    }
}
