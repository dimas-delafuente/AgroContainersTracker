using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Data.Entities
{
    public enum PalotType
    {
        Box,
        Palot
    }

    public class Palot
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ArrivalNumber { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        [Required]
        public double Weight { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public FruitGrower FruitGrower { get; set; }

        [Required]
        public FruitBuyer FruitBuyer { get; set; }

        [Required]
        public Fruit Fruit { get; set; }

        [Required]
        public PalotType Type { get; set; }

    }
}
