using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Data.Entities
{

    public class PalotEntity
    {
        [Key]
        public string PalotId { get; set; }

        [Required]
        public string ArrivalNumber { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        [Required]
        public double Weight { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Container))]
        public int ContainerId { get; set; }

        [Required]
        public ContainerEntity Container { get; set; }

        [ForeignKey(nameof(FruitGrower))]
        public Guid FruitGrowerId { get; set; }

        [Required]
        public FruitVendorEntity FruitGrower { get; set; }

        [ForeignKey(nameof(FruitBuyer))]
        public Guid FruitBuyerId { get; set; }

        [Required]
        public FruitVendorEntity FruitBuyer { get; set; }

        [ForeignKey(nameof(Fruit))]
        public Guid FruitId { get; set; }
        [Required]
        public FruitEntity Fruit { get; set; }

        [Required]
        public int Type { get; set; }

    }
}
