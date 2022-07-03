using AgroContainerTracker.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Entities
{
    public class Rate : IAggregate
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

        public virtual IEnumerable<Customer> Customers { get; set; }

    }
}
