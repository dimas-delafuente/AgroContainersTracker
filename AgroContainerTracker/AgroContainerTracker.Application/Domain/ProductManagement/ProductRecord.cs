using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain.ProductManagement
{
    public class ProductRecord
    {
        public int ProductRecordId { get; set; }
        public string ReferenceNumber { get; set; }

        public Weighing Weighing { get; set; }

        public Campaign Campaign { get; set; }

        public Input Input { get; set; }

        public  Fruit Fruit { get; set; }

        public  Storage Storage { get; set; }

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

    public class AddProductRecordRequest
    {

        public int WeighingId { get; set; }

        public int CampaignId { get; set; }

        public int InputNumber { get; set; }

        public int FruitId { get; set; }

        public int StorageId { get; set; }
        public int Quantity { get; set; }
        public int PackagingId { get; set; }
        public bool IsOwnPackaging { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public int SellerId { get; set; }

        public int BuyerId { get; set; }
    }
}
