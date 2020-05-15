
using System;
using System.ComponentModel.DataAnnotations;
using AgroContainerTracker.Domain.Rates;

namespace AgroContainerTracker.Domain.Companies
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

    public class AddCustomerRequest : AddCompanyBaseRequest
    {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int CustomerNumber { get; set; }

        [Required]
        public int? RateId { get; set; }

        public string BankAccount { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public int CustomerNumber { get; set; }

        public string Name { get; set; }

    }
}
