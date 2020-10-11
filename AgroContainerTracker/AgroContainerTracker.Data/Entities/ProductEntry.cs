using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public class ProductEntryEntity
    {
        public int CampaingId { get; set; }
        public int ProductEntryNumber { get; set; }

        public DateTime EntryDate { get; set; }
        public bool Closed { get; set; }
        public bool HasProductExit { get; set; }

        public int? BuyerId { get; set; }
        public int? PayerId { get; set; }

        public string Observations { get; set; }
        public bool HasHail { get; set; }
        public bool HasPlague { get; set; }
        public bool HasSecondPasses { get; set; }

        public virtual CustomerEntity Buyer { get; set; }
        public virtual CustomerEntity Payer { get; set; }
        public virtual ICollection<ProductEntrySellerEntity> Sellers { get; set; }
        public virtual ICollection<ProductWeighingEntity> ProductWeighings { get; set; }
        public virtual ICollection<ProductRecordEntity> ProductRecords { get; set; }

    }
}
