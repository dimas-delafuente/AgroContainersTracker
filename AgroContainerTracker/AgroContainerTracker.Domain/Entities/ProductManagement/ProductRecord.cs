namespace AgroContainerTracker.Domain.Entities
{
    public class ProductRecord
    {
        public int ProductRecordId { get; set; }
        public string ReferenceNumber { get; set; }

        public ProductWeighing ProductWeighing { get; set; }

        public Campaing Campaing { get; set; }

        public ProductEntry ProductEntry { get; set; }

        public  Fruit Fruit { get; set; }

        public  ColdRoom ColdRoom { get; set; }

        public int Quantity { get; set; }
        public bool IsOwnPackaging { get; set; }
        public Packaging Packaging { get; set; }

        public long? ProductExitId { get; set; }
        public int TotalDaysStored { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public Customer Seller { get; set; }

        public Customer Buyer { get; set; }
    }
}
