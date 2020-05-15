using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class FruitEntity
    {
        public FruitEntity()
        {
            Palots = new HashSet<PalotEntity>();
        }

        public int FruitId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PalotEntity> Palots { get; set; }
    }
}
