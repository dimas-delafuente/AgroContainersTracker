namespace AgroContainerTracker.Domain
{
    public class Carrier : IAggregate
    {
        public Company Company { get; set; } = null!;
        public SanitaryInfo SanitaryInfo { get; set; } = null!;
        public IEnumerable<Vehicle> Vehicles { get; } = null!;
        public IEnumerable<Carriage> Carriages { get; } = null!;
        public IEnumerable<Driver> Drivers { get; } = null!;
    }
}
