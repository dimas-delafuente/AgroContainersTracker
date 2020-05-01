using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Data.Entities
{
    public enum PackagingMaterial
    {
        Wood,
        Plastic
    }

    public enum PackaginType
    {
        Palot,
        Palet,
        Box
    }

    public class PackagingEntity
    {
        [Key]
        public int PackagingId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingMaterial))]
        public PackagingMaterial Material { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double Weight { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public CustomerEntity Customer { get; set; }

        [MaxLength(15)]
        public string Color { get; set; }

        [Required]
        [EnumDataType(typeof(PackaginType))]
        public PackaginType Type { get; set; }

        public bool Active { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Total { get; set; }

    }
}
