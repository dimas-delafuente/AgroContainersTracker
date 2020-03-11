using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Data.Entities
{
    public class FruitVendorEntity
    {
        [Key]
        public Guid FruitVendorId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
