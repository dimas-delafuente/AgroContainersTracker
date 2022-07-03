using AgroContainerTracker.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Entities
{
    public class PackagingMovement : IAggregate
    {
        public int PackagingMovementId { get; set; }

        public int PackagingId { get; set; }

        [EnumDataType(typeof(PackagMovementOperation))]
        public PackagMovementOperation Operation { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Amount { get; set; }

        public DateTime Created { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Total { get; set; }

        public Customer Customer { get; set; }

        public Packaging Packaging { get; set; }
    }
}
