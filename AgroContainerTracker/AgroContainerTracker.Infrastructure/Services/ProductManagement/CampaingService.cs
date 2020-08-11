using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.ProductManagement;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class CampaingService : ICampaingService
    {
        private readonly ILogger<CampaingService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CampaingService(ApplicationContext context, IMapper mapper, ILogger<CampaingService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Campaing>> GetAllAsync()
        {
            try
            {
                IEnumerable<CampaingEntity> entities = await _context.Campaings
                    .AsNoTracking()
                    .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Campaing>>(entities);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all campaings.", e.Message);
            }

            return new List<Campaing>();
        }

        public async Task<int> GetCampaingNextEntryId(int campaingId)
        {
            int maxEntryNumber = 0;

            try
            {
                maxEntryNumber = await _context.ProductEntries
                    .AsNoTracking()
                    .Where(x => x.CampaingId.Equals(campaingId))
                    .MaxAsync(x => x.ProductEntryNumber)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving next campaing entry number.", e.Message);
            }

            return maxEntryNumber + 1;
        }
    }
}
