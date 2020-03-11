using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain
{
    public class AddPalotRequest
    {
        [Required]
        public string PalotId { get; set; }

        [Required]
        public string ArrivalNumber { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public int ContainerId { get; set; }

        [Required]
        public Guid FruitGrowerId { get; set; }

        [Required]
        public Guid FruitBuyerId { get; set; }


        [Required]
        public Guid FruitId { get; set; }

    }
}
