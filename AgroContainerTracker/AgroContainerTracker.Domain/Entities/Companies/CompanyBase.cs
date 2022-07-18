namespace AgroContainerTracker.Domain
{
    public class Company : Entity<int>, IAggregate
    {
        public string CompanyCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Mobile { get; set; }

        public Address Address { get; set; } = null!;

        public string? Description { get; set; }
    }
}
