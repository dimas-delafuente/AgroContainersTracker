using System;
using System.Collections.Generic;
using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain.Packagings;

namespace AgroContainerTracker.Domain.Reports
{
    public class PackagingReport : ReportData
    {
        public Customer Customer { get; set; }

        public DateTime InitDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<PackagingMovement> PackagingMovements { get; set; }

    }
}
