using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class CustomerEntity
    {
        public CustomerEntity()
        {
            PalotsBuyer = new HashSet<PalotEntity>();
            PalotsSeller = new HashSet<PalotEntity>();
        }

        public int CustomerId { get; set; }
        public int CustomerNumber { get; set; }
        public string CompanyCode { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int? RateId { get; set; }
        public string BankAccount { get; set; }

        public virtual CountryEntity Country { get; set; }
        public virtual RateEntity Rate { get; set; }
        public virtual ICollection<PalotEntity> PalotsBuyer { get; set; }
        public virtual ICollection<PalotEntity> PalotsSeller { get; set; }
        public virtual ICollection<PackagingEntity> Packagings { get; set; }
        public virtual ICollection<PackagingMovementEntity> PackagingMovements { get; set; }

    }
}
