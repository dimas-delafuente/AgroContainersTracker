using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public class ProductWeighingEntity
    {
        public int ProductWeighingId { get; set; }

        public int CampaingId { get; set; }
        public virtual CampaingEntity Campaing { get; set; }

        public int ProductEntryNumber { get; set; }
        public virtual ProductEntryEntity ProductEntry { get; set; }

        public int? FruitId { get; set; }
        public virtual FruitEntity Fruit { get; set; }

        public int? ColdRoomId { get; set; }
        public virtual ColdRoomEntity ColdRoom { get; set; }

        public int? RateId { get; set; }
        public virtual RateEntity Rate { get; set; }

        public int? SellerId { get; set; }
        public virtual CustomerEntity Seller { get; set; }

        public int? BuyerId { get; set; }
        public virtual CustomerEntity Buyer { get; set; }

        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TareWeight { get; set; }

        public DateTime? ExitDate { get; set; }
        public int TotalLabels { get; set; }

        public virtual ICollection<ProductRecordEntity> ProductRecords { get; set; }

    }
}
