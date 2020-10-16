using AgroContainerTracker.Core.Reports;
using AgroContainerTracker.Core.Services.Reports;
using AgroContainerTracker.Domain.Reports;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class LabelReportFactory : ILabelReportFactory
    {
        private readonly IEnumerable<ILabelReportService> labelReportServices;
        public LabelReportFactory(IServiceProvider serviceProvider)
        {
            this.labelReportServices = serviceProvider.GetServices<ILabelReportService>() ?? throw new ArgumentNullException("No implementations found for ILabelReportService");
        }
        public async Task<byte[]> BuildReport(LabelReport labelReport, LabelType labelType)
        {
            ILabelReportService labelReportService;

            switch(labelType)
            {
                case LabelType.A5:
                    labelReportService = this.labelReportServices.FirstOrDefault(x => x.GetType().Equals(typeof(A5LabelReportService)));
                    break;

                case LabelType.CUSTOM:
                    labelReportService = this.labelReportServices.FirstOrDefault(x => x.GetType().Equals(typeof(StickerLabelReportService)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"The label type {labelType} is not a valid type.");
            }

            return await labelReportService.BuildReport(labelReport);
        }
    }
}
