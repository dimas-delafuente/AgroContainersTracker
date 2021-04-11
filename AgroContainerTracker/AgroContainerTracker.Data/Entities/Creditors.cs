using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class CreditorEntity
    {
        public int CreditorId { get; set; }
        public int CreditorNumber { get; set; }
        public string CompanyCode { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public virtual CountryEntity Country { get; set; }
    }
}
