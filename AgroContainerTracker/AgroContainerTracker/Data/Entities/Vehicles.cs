using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class VehicleEntity
    {
        public int VehicleId { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarriageRegistrationNumber { get; set; }
        public int CarrierId { get; set; }

        public virtual CarrierEntity CarrierCompany { get; set; }
    }
}
