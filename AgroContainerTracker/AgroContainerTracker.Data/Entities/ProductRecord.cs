namespace AgroContainerTracker.Data.Entities
{
    public class ProductRecordEntity
    {
        public int ProductRecordId { get; set; }

        public string ReferenceNumber { get; set; }

        public int ProductWeighingId { get; set; }
        public virtual ProductWeighingEntity ProductWeighing { get; set; }

        public int CampaingId { get; set; }
        public virtual CampaingEntity Campaing { get; set; }

        public int ProductEntryNumber { get; set; }
        public virtual ProductEntryEntity ProductEntry { get; set; }

        public int? FruitId { get; set; }
        public virtual FruitEntity Fruit { get; set; }

        public int? ColdRoomId { get; set; }
        public virtual ColdRoomEntity ColdRoom { get; set; }

        public int Quantity { get; set; }
        public int? PackagingId { get; set; }
        public bool IsOwnPackaging { get; set; }
        public virtual PackagingEntity Packaging { get; set; }

        public long? ProductExitId { get; set; }
        public int TotalDaysStored { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public int? SellerId { get; set; }
        public virtual CustomerEntity Seller { get; set; }

        public int? BuyerId { get; set; }
        public virtual CustomerEntity Buyer { get; set; }
    }
}
