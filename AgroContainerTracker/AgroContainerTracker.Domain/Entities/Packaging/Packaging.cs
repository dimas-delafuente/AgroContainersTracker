namespace AgroContainerTracker.Domain
{
    public class Packaging : Entity<int>, IAggregate
    {
        public string Code { get; set; } = null!;
        public Customer Owner { get; set; } = null!;
        public PackagingMaterial Material { get; set; } = null!;
        public PackagingType Type { get; set; } = null!;
        public Weight Weight { get; set; } = null!;
        public string? Description { get; set; }
        public string? Color { get; set; }
        public bool Active { get; set; }
        public int Total { get; set; }
    }
}
