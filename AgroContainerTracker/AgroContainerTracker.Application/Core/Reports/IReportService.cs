using System;
using AgroContainerTracker.Domain.Reports;

namespace AgroContainerTracker.Core.Services.Reports
{
    public interface IReportService<T> where T : ReportData
    {
        byte[] BuildReport(T reportData);
    }
}
