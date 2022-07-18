namespace AgroContainerTracker.Domain
{
    public class Weighing : Entity<int>, IAggregate
    {
        public Input Input { get; set; } = null!;
        public Fruit Fruit { get; set; } = null!;
        public Storage Storage { get; set; } = null!; // TODO: Move to ProductRecord?
        public Rate Rate { get; set; } = null!;
        public Customer Seller { get; set; } = null!;
        public Customer Buyer { get; set; } = null!;
        public Weight GrossWeight { get; set; } = null!; // TODO: Calculate instead of DB
        public Weight NetWeight { get; set; } = null!;
        public Weight TareWeight { get; set; } = null!;
        public DateTime? OutputDate { get; set; }
        public int TotalLabels { get; set; }
        public List<ProductRecord> ProductRecords { get; set; } = null!;
    }
}
