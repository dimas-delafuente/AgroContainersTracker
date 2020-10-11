using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class CarrierEntity
    {
        public CarrierEntity()
        {
            Drivers = new HashSet<DriverEntity>();
            Vehicles = new HashSet<VehicleEntity>();
        }

        public int CarrierId { get; set; }
        public int CarrierNumber { get; set; }
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
        public string SanitaryRegistrationNumber { get; set; }

        public virtual CountryEntity Country { get; set; }
        public virtual ICollection<DriverEntity> Drivers { get; set; }
        public virtual ICollection<VehicleEntity> Vehicles { get; set; }
        public virtual ICollection<CarriageEntity> Carriages { get; set; }
    }
}
