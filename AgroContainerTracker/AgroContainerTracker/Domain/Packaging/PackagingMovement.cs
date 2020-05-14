using System;
using System.ComponentModel.DataAnnotations;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Domain.Packagings
{
    public enum Operation
    {
        Add,
        Substract
    }

    public class PackagingMovement
    {
        public int PackagingMovementId { get; set; }

        public int PackagingId { get; set; }

        [EnumDataType(typeof(Operation))]
        public Operation Operation { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Amount { get; set; }

        public DateTime Created { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Total { get; set; }

        public Customer Customer { get; set; }
    }

    public class AddPackagingMovementRequest
    {
        [Required]
        public int PackagingId { get; set; }

        [Required]
        [EnumDataType(typeof(Operation))]
        public Operation Operation { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Amount { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
