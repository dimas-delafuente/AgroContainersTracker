namespace AgroContainerTracker.Domain
{
    public class Carriage : Entity<int>
    {
        public string LicenseNumber { get; set; } = null!;
        public Carrier Carrier { get; set; } = null!;
    }
}