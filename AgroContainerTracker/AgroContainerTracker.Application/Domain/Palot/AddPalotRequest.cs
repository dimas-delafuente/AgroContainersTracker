using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain
{
    public class AddPalotRequest
    {
        [Required]
        public string PalotCode { get; set; }

        [Required]
        public string ArrivalNumber { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public int ColdRoomId { get; set; }

        [Required]
        public int SellerId { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [Required]
        public int FruitId { get; set; }

    }
}
