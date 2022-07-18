namespace AgroContainerTracker.Domain
{
    public class PackagingMovement : Entity<int>, IAggregate
    {
        public Packaging Packaging { get; set; } = null!;

        public PackagingMovementOperation Operation { get; set; }

        public int Quantity { get; set; }

        public DateTime Created { get; set; }

        public int Total { get; set; }

        public Customer? Receiver { get; set; }
    }
}
