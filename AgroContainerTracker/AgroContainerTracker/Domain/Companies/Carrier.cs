using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Companies
{
    public class Carrier : CompanyBase
    {
        public int CarrierId { get; set; }

        public int CarrierNumber { get; set; }

        public string SanitaryRegistrationNumber { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }

        public IEnumerable<Driver> Drivers { get; set; }
    }

    public class Vehicle
    {
        public int CarrierId { get; set; }

        public string RegistrationNumber { get; set; }

        public string CarriageRegistrationNumber { get; set; }
    }

    public class Driver
    {

        public int CarrierId { get; set; }

        public string Name { get; set; }

        public string IdentificationNumber { get; set; }

    }

    public class AddCarrierRequest : AddCompanyBaseRequest
    {
        public AddCarrierRequest()
        {
            Vehicles = new List<Vehicle>();
            Drivers = new List<Driver>();
        }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int CarrierNumber { get; set; }

        public string SanitaryRegistrationNumber { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<Driver> Drivers { get; set; }

    }
}
