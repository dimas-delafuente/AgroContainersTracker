using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Entities
{
    public class Customer : CompanyBase
    {
        public Customer()
        {
            Country = new Country();
            Rate = new Rate();
        }

        public int CustomerId { get; set; }

        [Required]
        public int CustomerNumber { get; set; }

        [Required]
        public Rate Rate { get; set; }

        public string BankAccount { get; set; }

    }
}
