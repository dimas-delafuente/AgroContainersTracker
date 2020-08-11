using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class FruitEntity
    {
        public FruitEntity()
        {
        }

        public int FruitId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductWeighingEntity> ProductWeighings { get; set; }
        public virtual ICollection<ProductRecordEntity> ProductRecords { get; set; }


    }
}
