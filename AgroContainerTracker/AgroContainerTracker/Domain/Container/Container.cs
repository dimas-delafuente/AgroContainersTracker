using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Domain
{
    public class Container
    {
        [Required]
        public int ContainerId { get; set; }

        [Required]
        public double Size { get; set; }

        public List<Palot> Palots { get; set; }
    }
}
