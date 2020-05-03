using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Companies
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

    public class AddCreditorRequest : AddCompanyBaseRequest
    {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int CreditorNumber { get; set; }

    }
}
