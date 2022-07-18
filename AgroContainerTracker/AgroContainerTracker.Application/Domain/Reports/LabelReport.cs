using AgroContainerTracker.Domain.ProductManagement;

namespace AgroContainerTracker.Domain.Reports
{
    public class LabelReport : ReportData
    {
        public Weighing Weighing { get; set; }
    }

    public enum LabelType
    {
        A5,
        CUSTOM
    }
}
