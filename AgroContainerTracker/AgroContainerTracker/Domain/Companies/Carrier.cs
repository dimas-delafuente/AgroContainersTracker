using System.Collections.Generic;

namespace AgroContainerTracker.Domain.Companies
{
    public class Carrier : CompanyBase
    {
        public string SanitaryRegistrationNumber { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }

        public IEnumerable<Driver> Drivers { get; set; }
    }

    public class Vehicle
    {
        public int CompanyId { get; set; }

        public string RegistrationNumber { get; set; }

        public string CarriageRegistrationNumber { get; set; }
    }

    public class Driver
    {

        public int CompanyId { get; set; }

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

        public string SanitaryRegistrationNumber { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<Driver> Drivers { get; set; }

    }
}
