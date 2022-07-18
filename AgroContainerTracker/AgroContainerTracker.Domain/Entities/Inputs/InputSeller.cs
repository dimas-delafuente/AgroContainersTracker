namespace AgroContainerTracker.Domain
{
    public class InputSeller : IAggregate
    {
        public Input Input { get; set; } = null!;
        public Customer Seller { get; set; } = null!;
    }
}