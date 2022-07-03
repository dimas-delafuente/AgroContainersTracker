using AgroContainerTracker.Domain.Entities;

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
