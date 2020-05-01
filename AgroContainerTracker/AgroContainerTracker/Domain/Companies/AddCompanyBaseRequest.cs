using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Companies
{
    public class AddCompanyBaseRequest
    {
        [Required]
        public int CompanyNumber { get; set; }

        [Required]
        [MaxLength(9)]
        public string CompanyCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string ContactName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(9)]
        public string Phone { get; set; }

        [MaxLength(9)]
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }

        public string State { get; set; }

        [MaxLength(5)]
        public string PostalCode { get; set; }
        public int CountryId { get; set; }

        public string Description { get; set; }

    }
}