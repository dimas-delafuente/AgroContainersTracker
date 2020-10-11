using AgroContainerTracker.Domain.Reports;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services.Reports
{
    public interface ILabelReportFactory
    {
        Task<byte[]> BuildReport(LabelReport labelReport, LabelType labelType);
    }
}
