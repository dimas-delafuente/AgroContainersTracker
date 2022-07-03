namespace AgroContainerTracker.Domain.Entities
{
    public class Creditor : CompanyBase
    {
        public Creditor()
        {
            Country = new Country();
        }

        public int CreditorId { get; set; }

        public int CreditorNumber { get; set; }
    }
}
