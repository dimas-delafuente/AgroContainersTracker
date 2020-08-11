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
using System.Text.Json;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class ProductEntryService : IProductEntryService
    {
        private readonly ILogger<ProductEntryService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ProductEntryService(ApplicationContext context, IMapper mapper, ILogger<ProductEntryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductEntry> AddAsync(ProductEntry productEntry)
        {
            try
            {
                if (productEntry == null)
                    throw new ArgumentNullException();

                ProductEntryEntity entity = _mapper.Map<ProductEntryEntity>(productEntry);
                var addResponse = await _context.ProductEntries.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<ProductEntry>(addResponse.Entity) : null;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Product Entry: {productEntry}", e.Message, JsonSerializer.Serialize(productEntry));
            }

            return null;
        }

        public async Task<ProductEntry> GetAsync(int campaingId, int productEntryNumber)
        {
            try
            {
                if (campaingId < 0 || productEntryNumber < 0)
                    throw new ArgumentOutOfRangeException();

                ProductEntryEntity entity = await _context.ProductEntries
                    .AsNoTracking()
                    .Include(x => x.Buyer)
                    .Include(x => x.Payer)
                    .Include(x => x.Sellers)
                        .ThenInclude(s => s.Customer)
                    .FirstOrDefaultAsync(x => x.CampaingId.Equals(campaingId) && x.ProductEntryNumber.Equals(productEntryNumber))
                    .ConfigureAwait(false);

                return _mapper.Map<ProductEntry>(entity);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving ProductEntry. CampaingId:{campaingId} - ProductEntryNumber: {productEntryNumber}", 
                    e.Message, campaingId, productEntryNumber);
            }

            return null;
        }

        public async Task<List<ProductEntry>> GetByCampaingIdAsync(int campaingId)
        {
            try
            {
                if (campaingId < 0)
                    throw new ArgumentOutOfRangeException();

                IEnumerable<ProductEntryEntity> entities = await _context.ProductEntries
                    .AsNoTracking()
                    .Include(x => x.Buyer)
                    .Include(x => x.Payer)
                    .Include(x => x.Sellers)
                        .ThenInclude(s => s.Customer)
                    .Where(x => x.CampaingId.Equals(campaingId))
                    .ToListAsync()
                    .ConfigureAwait(false);

                return _mapper.Map<List<ProductEntry>>(entities);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all ProductEntry. CampaingId:{campaingId}",
                                    e.Message, campaingId);
            }

            return new List<ProductEntry>();
        }
    }
}
