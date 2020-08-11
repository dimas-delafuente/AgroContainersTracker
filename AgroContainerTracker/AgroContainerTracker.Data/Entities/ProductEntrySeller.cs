namespace AgroContainerTracker.Data.Entities
{
    public class ProductEntrySellerEntity
    {
        public int ProductEntryNumber { get; set; }
        public virtual ProductEntryEntity ProductEntry { get; set; }

        public int CampaingId { get; set; }
        public virtual CampaingEntity Campaing { get; set; }

        public int CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}
