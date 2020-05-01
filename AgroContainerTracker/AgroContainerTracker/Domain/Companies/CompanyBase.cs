using System;
namespace AgroContainerTracker.Domain.Companies
{
    public class CompanyBase
    {
        public int CompanyId { get; set; }

        public int CompanyNumber { get; set; }

        public string CompanyCode { get; set; }

        public string Name { get; set; }

        public string ContactName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
        public Country Country { get; set; }

        public string Description { get; set; }
    }
}
