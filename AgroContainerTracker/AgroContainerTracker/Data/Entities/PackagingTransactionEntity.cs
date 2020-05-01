using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Data.Entities
{
    public enum Transaction
    {
        Add,
        Substract
    }

    public class PackagingTransactionEntity
    {
        [Key]
        public int PackagingTransactionId { get; set; }

        [Required]
        public int PackageEntityId { get; set; }

        [Required]
        [ForeignKey("PackageEntityId")]
        public PackagingEntity Packaging { get; set; }

        [Required]
        [EnumDataType(typeof(Transaction))]
        public Transaction Transaction { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Amount { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Total { get; set; }

    }
}
