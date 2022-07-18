namespace AgroContainerTracker.Domain
{
    public class ProductRecord : Entity<int>, IAggregate
    {
        public string ReferenceNumber { get; set; } = null!;
        public Weighing Weighing { get; set; } = null!;
        public Output? Output { get; set; }
        public Fruit Fruit { get; set; } = null!;
        public Storage Storage { get; set; } = null!;
        public Packaging Packaging { get; set; } = null!;
        public Customer Seller { get; set; } = null!;
        public Customer Buyer { get; set; } = null!;
        public Rate Rate { get; set; } = null!;
        public int Quantity { get; set; }
        public bool IsOwnPackaging { get; set; }
        public int TotalDaysStored { get; set; } // TODO: Calculate from InputDate - OutputDate
        public Weight GrossWeight { get; set; } = null!;
        public Weight NetWeight { get; set; } = null!;
        public Weight TareWeight { get; set; } = null!;
    }
}
