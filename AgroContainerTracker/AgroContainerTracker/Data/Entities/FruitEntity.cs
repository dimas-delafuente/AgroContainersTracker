using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Data.Entities
{
    public class FruitEntity
    {
        [Key]
        public Guid FruitId { get; set; }

        public string Name { get; set; }
    }
}
