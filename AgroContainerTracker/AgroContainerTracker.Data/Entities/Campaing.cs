using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public class CampaingEntity
    {
        public int CampaingId { get; set; }

        public virtual ICollection<ProductEntryEntity> ProductEntries { get; set; }
    }
}
