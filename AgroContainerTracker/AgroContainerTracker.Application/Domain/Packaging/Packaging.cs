using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Domain.Packagings
{
    public enum PackagingMaterial
    {
        Madera,
        Plastico
    }

    public enum PackagingType
    {
        Palot = 'P',
        Palet = 'C',
        Caja
    }

    public class Packaging
    {
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

    public class AddPackagingRequest
    {
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

        [Required]
        public int CustomerId { get; set; }

        public string Color { get; set; }

        [Required]
        [EnumDataType(typeof(PackagingType))]
        public PackagingType Type { get; set; }

        public bool Active { get; set; }

        [Required]
        public int Total { get; set; }
    }
}
