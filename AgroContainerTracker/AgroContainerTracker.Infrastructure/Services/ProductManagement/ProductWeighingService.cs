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
    public class ProductWeighingService : IProductWeighingService
    {
        private readonly ILogger<ProductWeighingService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ProductWeighingService(ApplicationContext context, IMapper mapper, ILogger<ProductWeighingService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductWeighing> AddAsync(AddProductWeighingRequest productWeighing)
        {
            if (productWeighing == null || !(productWeighing.ProductRecords?.Count > 0))
                throw new ArgumentNullException();

            try
            {
                
                ProductWeighingEntity entity = _mapper.Map<ProductWeighingEntity>(productWeighing);

                var addResponse = await _context.ProductWeighings.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<ProductWeighing>(addResponse.Entity) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Product Weighing: {productWeighing}", e.Message, JsonSerializer.Serialize(productWeighing));
            }

            return null;


        }

        public async Task<List<ProductWeighing>> GetAllAsync(int campaingId, int productEntryNumber)
        {
            try
            {
                if (campaingId < 0)
                    throw new ArgumentOutOfRangeException();

                IEnumerable<ProductWeighingEntity> entities = await _context.ProductWeighings
                    .AsNoTracking()
                    .Include(x => x.ProductRecords)
                    .Where(x => x.CampaingId.Equals(campaingId) && x.ProductEntryNumber.Equals(productEntryNumber))
                    .ToListAsync()
                    .ConfigureAwait(false);

                return _mapper.Map<List<ProductWeighing>>(entities);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all Product Weighings. CampaingId:{campaingId}, ProductEntryNumber:{productEntryNumber}",
                                    e.Message, campaingId, productEntryNumber);
            }

            return new List<ProductWeighing>();
        }

    }
}
