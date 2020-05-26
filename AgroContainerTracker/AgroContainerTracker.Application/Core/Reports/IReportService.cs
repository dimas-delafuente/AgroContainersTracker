using System;
namespace AgroContainerTracker.Core.Services.Reports
{
    public interface IReportService
    {
        byte[] BuildReport();
    }
}
