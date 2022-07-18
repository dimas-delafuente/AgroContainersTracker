namespace AgroContainerTracker.Domain
{
    public class Storage : Entity<int>, IAggregate
    {
        public int Number { get; private set; }
        public string? Description { get; private set; }
        public double? Surface { get; private set; }
        public Weight? Capacity { get; private set; }
        public double? Temperature { get; private set; }
        public double? Humidity { get; private set; }
    }
}
