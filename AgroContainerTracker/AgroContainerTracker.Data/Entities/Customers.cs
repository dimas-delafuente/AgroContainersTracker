using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class CustomerEntity
    {
        public CustomerEntity()
        {
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

        public virtual ICollection<PackagingEntity> Packagings { get; set; }
        public virtual ICollection<PackagingMovementEntity> PackagingMovements { get; set; }

        public virtual ICollection<ProductEntryEntity> BuyerProductEntries { get; set; }
        public virtual ICollection<ProductWeighingEntity> BuyerProductWeighings { get; set; }
        public virtual ICollection<ProductRecordEntity> BuyerProductRecords { get; set; }

        public virtual ICollection<ProductEntrySellerEntity> SellerProductEntries { get; set; }
        public virtual ICollection<ProductWeighingEntity> SellerProductWeighings { get; set; }
        public virtual ICollection<ProductRecordEntity> SellerProductRecords { get; set; }


        public virtual ICollection<ProductEntryEntity> PayerProductEntries { get; set; }

    }
}
