using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class RateEntity
    {
        public RateEntity()
        {
            Customers = new HashSet<CustomerEntity>();
        }

        public int RateId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double SecondaryValue { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; }
    }
}
