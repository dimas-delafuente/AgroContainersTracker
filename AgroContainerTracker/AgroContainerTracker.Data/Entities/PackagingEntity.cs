using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Data.Entities
{
    public enum PackagingMaterial
    {
        Madera,
        Plastico
    }

    public enum PackagingType
    {
        Palot,
        Palet,
        Caja
    }

    public class PackagingEntity
    {
        public int PackagingId { get; set; }

        public string Code { get; set; }

        [EnumDataType(typeof(PackagingType))]
        public PackagingType Type { get; set; }

        public string Description { get; set; }

        public int? CustomerId { get; set; }

        public string Color { get; set; }
        [EnumDataType(typeof(PackagingMaterial))]
        public PackagingMaterial Material { get; set; }


        public double Weight { get; set; }
        public int Total { get; set; }

        public bool Active { get; set; }

        public virtual CustomerEntity Owner { get; set; }
        public virtual ICollection<PackagingMovementEntity> PackagingMovements { get; set; }

        public virtual ICollection<ProductRecordEntity> ProductRecords { get; set; }

    }
}
