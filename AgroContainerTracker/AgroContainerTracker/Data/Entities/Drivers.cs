using System;
using System.Collections.Generic;

namespace AgroContainerTracker.Data.Entities
{
    public partial class DriverEntity
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public int CompanyId { get; set; }
        public int? CarrierCompanyId { get; set; }

        public virtual CarrierEntity CarrierCompany { get; set; }
    }
}
