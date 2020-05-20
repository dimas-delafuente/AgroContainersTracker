using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class CountryEntity
    {
        public CountryEntity()
        {
            Carriers = new HashSet<CarrierEntity>();
            Creditors = new HashSet<CreditorEntity>();
            Customers = new HashSet<CustomerEntity>();
            Suppliers = new HashSet<SupplierEntity>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CarrierEntity> Carriers { get; set; }
        public virtual ICollection<CreditorEntity> Creditors { get; set; }
        public virtual ICollection<CustomerEntity> Customers { get; set; }
        public virtual ICollection<SupplierEntity> Suppliers { get; set; }
    }
}
