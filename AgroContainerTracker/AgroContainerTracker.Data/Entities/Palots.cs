using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class PalotEntity
    {
        public int PalotId { get; set; }
        public string PalotCode { get; set; }
        public string ArrivalNumber { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public double Weight { get; set; }
        public bool IsActive { get; set; }
        public int ContainerId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int FruitId { get; set; }
        public int Type { get; set; }

        public virtual CustomerEntity Buyer { get; set; }
        public virtual ContainerEntity Container { get; set; }
        public virtual FruitEntity Fruit { get; set; }
        public virtual CustomerEntity Seller { get; set; }
    }
}
