using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Companies
{
    public class CompanyBase
    {

        [Required]
        [MaxLength(9)]
        public string CompanyCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(9)]
        public string Phone { get; set; }

        [MaxLength(9)]
        public string Mobile { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Locality { get; set; }

        [Required]
        public string State { get; set; }

        [MaxLength(5)]
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public Country Country { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }
}
