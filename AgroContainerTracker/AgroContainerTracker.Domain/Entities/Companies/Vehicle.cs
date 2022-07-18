namespace AgroContainerTracker.Domain
{
    public class Vehicle : Entity<int>, IAggregate
    {
        public string LicenseNumber { get; set; } = null!;
        public Carrier Carrier { get; set; } = null!;
    }
}