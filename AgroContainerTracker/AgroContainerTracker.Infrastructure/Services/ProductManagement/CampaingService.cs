using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ILogger<CampaignService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CampaignService(ApplicationContext context, IMapper mapper, ILogger<CampaignService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> GetCampaignNextEntryId(int CampaignId)
        {
            int maxEntryNumber = 0;

            try
            {
                maxEntryNumber = await _context.Inputs
                    .AsNoTracking()
                    .Where(x => x.CampaignId.Equals(CampaignId))
                    .MaxAsync(x => x.Id)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving next Campaign entry number.", e.Message);
            }

            return maxEntryNumber + 1;
        }

        public async Task<int> GetCampaignNextWeightingId(int CampaignId)
        {
            int maxWeightingNumber = 0;

            try
            {
                maxWeightingNumber = await _context.Weighings
                    .AsNoTracking()
                    .Where(x => x.CampaignId.Equals(CampaignId))
                    .MaxAsync(x => x.WeighingId)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving next Campaign Weighting number.", e.Message);
            }

            return maxWeightingNumber + 1;
        }
    }
}
