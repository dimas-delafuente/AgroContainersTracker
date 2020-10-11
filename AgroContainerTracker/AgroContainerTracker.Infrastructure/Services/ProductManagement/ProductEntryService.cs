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

        public async Task<bool> ExistsAsync(int campaingId, int productEntryNumber)
        {
            try
            {
                if (campaingId < 0 || productEntryNumber < 0)
                    throw new ArgumentOutOfRangeException();

                ProductEntryEntity entity = await _context.ProductEntries
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CampaingId.Equals(campaingId) && x.ProductEntryNumber.Equals(productEntryNumber))
                    .ConfigureAwait(false);

                return entity != null;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving ProductEntry. CampaingId:{campaingId} - ProductEntryNumber: {productEntryNumber}",
                    e.Message, campaingId, productEntryNumber);
            }

            return true;
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

        public async Task<bool> UpdateAsync(ProductEntry productEntry)
        {
            try
            {
                if (productEntry == null)
                    throw new ArgumentNullException();

                ProductEntryEntity entity = await _context.ProductEntries
                    .Include(x => x.Sellers)
                    .FirstOrDefaultAsync(x => x.CampaingId.Equals(productEntry.CampaingId) && x.ProductEntryNumber.Equals(productEntry.ProductEntryNumber))
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(productEntry, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Product Entry: {productEntry}", e.Message, JsonSerializer.Serialize(productEntry));
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int campaingId, int productEntryNumber)
        {
            if (campaingId <= 0 || productEntryNumber <= 0)
                throw new ArgumentOutOfRangeException();

            try
            {
                ProductEntryEntity productEntry = await _context.ProductEntries.FindAsync(campaingId, productEntryNumber).ConfigureAwait(false);

                if (productEntry != null)
                {
                    var removedEntity = _context.ProductEntries.Remove(productEntry);
                    if (removedEntity?.Entity != null && removedEntity.State.Equals(EntityState.Deleted))
                    {
                        int deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
                        return deleted > 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while removing ProductEntry. CampaingId:{campaingId}, ProductEntryNumber:{productEntryNumber}",
                                    e.Message, campaingId, productEntryNumber);
            }

            return false;
        }

        public async Task<bool> CloseEntryAsync(int campaingId, int productEntryNumber)
        {
            if (campaingId <= 0 || productEntryNumber <= 0)
                throw new ArgumentOutOfRangeException();

            try
            {

                ProductEntryEntity entity = await _context.ProductEntries
                        .Include(x => x.Sellers)
                        .FirstOrDefaultAsync(x => x.CampaingId.Equals(campaingId) && x.ProductEntryNumber.Equals(productEntryNumber))
                        .ConfigureAwait(false);
                if (entity != null)
                {
                    entity.Closed = true;
                    entity.EntryDate = DateTime.Now;
                    return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                }

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while closing Product Entry. CampaingId:{campaingId} - ProductEntryNumber: {productEntryNumber}", campaingId, productEntryNumber);
            }

            return false;
        }
    }
}
