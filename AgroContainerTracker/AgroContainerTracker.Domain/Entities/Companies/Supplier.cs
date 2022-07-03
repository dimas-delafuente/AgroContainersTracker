namespace AgroContainerTracker.Domain.Entities
{
    public class Supplier : CompanyBase
    {
        public Supplier()
        {
            Country = new Country();
        }
        public int SupplierId { get; set; }

        public int SupplierNumber { get; set; }
    }
}
