using System;
using System.ComponentModel.DataAnnotations;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Domain.Packagings
{
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

        public Packaging Packaging { get; set; }
    }

    public class AddPackagingMovementRequest
    {
        public int? PackagingId { get; set; }

        public Operation Operation { get; set; }

        public int Amount { get; set; }

        public DateTime Created { get; set; }

        public int? CustomerId { get; set; }
    }
}
