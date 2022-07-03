using AgroContainerTracker.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Entities
{
    public class Packaging : IAggregate
    {
        public int CustomerId { get; set; }
        public int PackagingId { get; set; }

        [Required]
        [MaxLength(8)]
        public string Code { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingMaterial))]
        public PackagingMaterial Material { get; set; }

        [Required]
        public double Weight { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public Customer Owner { get; set; }

        public string Color { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingType))]
        public PackagingType Type { get; set; }

        public bool Active { get; set; }

        [Required]
        public int Total { get; set; }

        public IEnumerable<PackagingMovement> PackagingMovements { get; set; }
    }
}
