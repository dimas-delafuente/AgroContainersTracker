using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Data.Entities
{
    public enum Operation
    {
        Add,
        Substract
    }

    public class PackagingMovementEntity
    {
        public int PackagingMovementId { get; set; }

        public int PackagingId { get; set; }

        public int? CustomerId { get; set; }

        [EnumDataType(typeof(Operation))]
        public Operation Operation { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Amount { get; set; }

        public DateTime Created { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Total { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual PackagingEntity Packaging { get; set; }
    }
}
