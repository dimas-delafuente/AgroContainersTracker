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

        public string PalotId { get; set; }

        public string ArrivalNumber { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        public double Weight { get; set; }

        public bool IsActive { get; set; }

        public int ContainerId { get; set; }

        public FruitVendor FruitGrower { get; set; }

        public FruitVendor FruitBuyer { get; set; }

        public Fruit Fruit { get; set; }

        public PalotType Type { get; set; }
    }
}
