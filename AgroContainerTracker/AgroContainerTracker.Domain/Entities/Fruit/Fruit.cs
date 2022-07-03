using AgroContainerTracker.Domain.Common;
using AgroContainerTracker.Domain.ProductManagement;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Entities
{
    public class Fruit : IAggregate
    {
        public int FruitId { get; set; }

        [Required]
        [MaxLength(5)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<ProductWeighing> ProductWeighings { get; set; }
        public virtual ICollection<ProductRecord> ProductRecords { get; set; }
    }
}