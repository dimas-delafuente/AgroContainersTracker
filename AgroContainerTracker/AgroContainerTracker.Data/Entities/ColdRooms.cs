using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class ColdRoomEntity
    {
        public ColdRoomEntity()
        {
        }

        public int ColdRoomId { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }
        public double Surface { get; set; }
        public double Capacity { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }


        public virtual ICollection<ProductWeighingEntity> ProductWeighings { get; set; }
        public virtual ICollection<ProductRecordEntity> ProductRecords { get; set; }

    }
}
