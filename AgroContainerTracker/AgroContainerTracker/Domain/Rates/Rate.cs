using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Rates
{
    public class Rate
    {
        public int RateId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public double Value { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public double SecondaryValue { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }
}
