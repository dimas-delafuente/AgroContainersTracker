
using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Companies
{
    public class Customer : CompanyBase
    {
        public int CustomerId { get; set; }

        public int CustomerNumber { get; set; }

        public Rate Rate { get; set; }

        public string BankAccount { get; set; }

    }

    public class AddCustomerRequest : AddCompanyBaseRequest
    {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int CustomerNumber { get; set; }

        public int? RateId { get; set; }

        public string BankAccount { get; set; }
    }
}
