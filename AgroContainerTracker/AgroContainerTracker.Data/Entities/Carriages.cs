namespace AgroContainerTracker.Data.Entities
{
    public class CarriageEntity
    {
        public int CarriageId { get; set; }
        public string CarriageRegistrationNumber { get; set; }
        public int CarrierId { get; set; }

        public virtual CarrierEntity CarrierCompany { get; set; }
    }
}
