using System;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Data.Entities
{
    public class Container
    {
        [Key]
        public int Id { get; set; }

        public double? Size { get; set; }

        public Palot[, ,] Palots { get; set; }
    }
}
