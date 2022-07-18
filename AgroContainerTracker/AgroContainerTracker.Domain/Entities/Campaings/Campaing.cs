namespace AgroContainerTracker.Domain
{
    public class Campaign : Entity<int>, IAggregate
    {
        public IEnumerable<Input>? Inputs { get; set; }
    }
}