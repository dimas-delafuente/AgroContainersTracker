namespace AgroContainerTracker.Domain
{
    public class Fruit : Entity<int>, IAggregate
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}