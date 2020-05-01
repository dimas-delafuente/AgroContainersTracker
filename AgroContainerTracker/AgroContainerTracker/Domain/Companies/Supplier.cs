using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Companies
{
    public class Supplier : CompanyBase
    {
        public int SupplierId { get; set; }

        public int SupplierNumber { get; set; }
    }

    public class AddSupplierRequest : AddCompanyBaseRequest
    {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int SupplierNumber { get; set; }
    }
}
