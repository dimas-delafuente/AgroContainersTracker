using AgroContainerTracker.Domain.Entities;

namespace AgroContainerTracker.Domain.Reports
{
    public class LabelReport : ReportData
    {
        public ProductWeighing ProductWeighing { get; set; }
    }

    public enum LabelType
    {
        A5,
        CUSTOM
    }
}
