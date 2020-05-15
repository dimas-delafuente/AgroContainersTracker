using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class ContainerEntity
    {
        public ContainerEntity()
        {
            Palots = new HashSet<PalotEntity>();
        }

        public int ContainerId { get; set; }
        public double Size { get; set; }
        public double Temperature { get; set; }

        public virtual ICollection<PalotEntity> Palots { get; set; }
    }
}
