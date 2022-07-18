using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Domain
{
    public class Palot : Entity<int>, IAggregate
    {
        public enum PalotType
        {
            Box,
            Palot
        }

        public string PalotCode { get; set; } = null!;

        public string? ArrivalNumber { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        public double Weight { get; set; }

        public bool IsActive { get; set; }

        public int StorageId { get; set; }

        public int SellerId { get; set; }

        public int BuyerId { get; set; }

        public int FruitId { get; set; }

        public PalotType Type { get; set; }
    }
}
