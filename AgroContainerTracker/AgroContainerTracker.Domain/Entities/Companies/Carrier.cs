namespace AgroContainerTracker.Domain.Entities
{
    public class Carrier : CompanyBase
    {
        public Carrier()
        {
            Country = new Country();
            Vehicles = new List<Vehicle>();
            Carriages = new List<Carriage>();
            Drivers = new List<Driver>();
        }

        public int CarrierId { get; set; }

        public int CarrierNumber { get; set; }

        public string SanitaryRegistrationNumber { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public List<Carriage> Carriages { get; set; }

        public List<Driver> Drivers { get; set; }
    }

    public class Vehicle
    {
        public int CarrierId { get; set; }
        public string RegistrationNumber { get; set; }

    }

    public class Carriage
    {
        public int CarrierId { get; set; }
        public string CarriageRegistrationNumber { get; set; }
    }

    public class Driver
    {

        public int CarrierId { get; set; }

        public string Name { get; set; }

        public string IdentificationNumber { get; set; }

    }
}
