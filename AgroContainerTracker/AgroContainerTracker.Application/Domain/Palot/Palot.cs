using AgroContainerTracker.Domain.Companies;
using System;

namespace AgroContainerTracker.Domain
{
    public class Palot
    {
        public enum PalotType
        {
            Box,
            Palot
        }
        public int PalotId { get; set; }

        public string PalotCode { get; set; }

        public string ArrivalNumber { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        public double Weight { get; set; }

        public bool IsActive { get; set; }

        public int StorageId { get; set; }

        public Customer Seller { get; set; }

        public Customer Buyer { get; set; }

        public Fruit Fruit { get; set; }

        public PalotType Type { get; set; }
    }
}
