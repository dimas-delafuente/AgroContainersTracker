
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Domain.Fruits
{
    public class Fruit
    {
        public int FruitId { get; set; }

        [Required]
        [MaxLength(5)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }

    public class AddFruitRequest
    {
        [Required]
        [MaxLength(5)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}