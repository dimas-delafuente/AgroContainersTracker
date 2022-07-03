namespace AgroContainerTracker.Domain.Entities
{
    public class ProductWeighing
    {
        public int ProductWeighingId { get; set; }

        public int CampaingId { get; set; }

        public int ProductEntryNumber { get; set; }
        public ProductEntry ProductEntry { get; set; }

        public Fruit Fruit { get; set; }

        public ColdRoom ColdRoom { get; set; }

        public Rate Rate { get; set; }

        public Customer Seller { get; set; }

        public Customer Buyer { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public DateTime? ExitDate { get; set; }
        public int TotalLabels { get; set; }

        public List<ProductRecord> ProductRecords { get; set; }
    }
}
