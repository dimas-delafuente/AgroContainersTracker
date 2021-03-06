﻿using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class SupplierEntity
    {
        public int SupplierId { get; set; }
        public int SupplierNumber { get; set; }
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

        public virtual CountryEntity Country { get; set; }
    }
}
