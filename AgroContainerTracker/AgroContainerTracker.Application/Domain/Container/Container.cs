using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroContainerTracker.Domain
{
    public class Container
    {
        public int ContainerId { get; set; }

        public double Size { get; set; }

        public List<Palot> Palots { get; set; }
    }
}
