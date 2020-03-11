using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgroContainerTracker.Data.Entities
{
    public class ContainerEntity
    {
        [Key]
        public int ContainerId { get; set; }

        public double Size { get; set; }

        public ICollection<PalotEntity> Palots { get; set; }
    }
}
