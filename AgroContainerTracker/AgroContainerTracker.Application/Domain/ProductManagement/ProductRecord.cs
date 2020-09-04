using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain.Fruits;
using AgroContainerTracker.Domain.Packagings;

namespace AgroContainerTracker.Domain.ProductManagement
{
    public class ProductRecord
    {
        public int ProductRecordId { get; set; }

        public ProductWeighing ProductWeighing { get; set; }

        public Campaing Campaing { get; set; }

        public ProductEntry ProductEntry { get; set; }

        public  Fruit Fruit { get; set; }

        public  ColdRoom ColdRoom { get; set; }

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

    public class AddProductRecordRequest
    {

        public int ProductWeighingId { get; set; }

        public int CampaingId { get; set; }

        public int ProductEntryNumber { get; set; }

        public int FruitId { get; set; }

        public int ColdRoomId { get; set; }

        public int PackagingId { get; set; }
        public bool IsOwnPackaging { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public int SellerId { get; set; }

        public int BuyerId { get; set; }
    }
}
