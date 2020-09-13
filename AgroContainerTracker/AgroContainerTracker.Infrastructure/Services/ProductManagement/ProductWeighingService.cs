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

                    if (created)
                    {
                        var productRecords = GetProductRecords(productWeighing, addResponse.Entity.ProductWeighingId);
                        addResponse.Entity.ProductRecords = (ICollection<ProductRecordEntity>)await AddProductRecords(productRecords);
                    }

                    return created ? await GetByIdAsync(addResponse.Entity.ProductWeighingId) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Product Weighing: {productWeighing}", e.Message, JsonSerializer.Serialize(productWeighing));
            }

            return null;
        }

        public async Task<ProductWeighing> UpdateAsync(AddProductWeighingRequest productWeighing)
        {
            if (productWeighing == null || !(productWeighing.ProductRecords?.Count > 0))
                throw new ArgumentNullException();

            try
            {

                ProductWeighingEntity entity = await _context.ProductWeighings
                    .Include(x => x.ProductRecords)
                    .FirstOrDefaultAsync(x => x.ProductWeighingId.Equals(productWeighing.ProductWeighingId))
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(productWeighing, entity);
                    entity.ProductRecords = (ICollection<ProductRecordEntity>)GetProductRecords(productWeighing, entity.ProductWeighingId);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    return await GetByIdAsync(entity.ProductWeighingId);
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Product Weighing: {productWeighing}", e.Message, JsonSerializer.Serialize(productWeighing));
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
                        .ThenInclude(x => x.Packaging)
                    .Include(x => x.ColdRoom)
                    .Include(x => x.Fruit)
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

        public async Task<ProductWeighing> GetByIdAsync(int productWeighingId)
        {
            try
            {
                if (productWeighingId < 0)
                    throw new ArgumentOutOfRangeException();

                ProductWeighingEntity entities = await _context.ProductWeighings
                    .AsNoTracking()
                    .Include(x => x.ProductRecords)
                        .ThenInclude(p => p.Packaging)
                    .Include(x => x.Buyer)
                    .Include(x => x.Seller)
                    .Include(x => x.Fruit)
                    .Include(x => x.ColdRoom)
                    .Include(x => x.Rate)
                    .FirstOrDefaultAsync(x => x.ProductWeighingId.Equals(productWeighingId))
                    .ConfigureAwait(false);

                return _mapper.Map<ProductWeighing>(entities);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Product Weighings. ProductWeighingId:{productWeighingId}.}",
                                    e.Message, productWeighingId);
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int productWeighingId)
        {
            try
            {
                ProductWeighingEntity productWeighing = await _context.ProductWeighings.FindAsync(productWeighingId).ConfigureAwait(false);

                if (productWeighing != null)
                {
                    var removedEntity = _context.ProductWeighings.Remove(productWeighing);
                    if (removedEntity?.Entity != null && removedEntity.State.Equals(EntityState.Deleted))
                    {
                        int deleted = await _context.SaveChangesAsync().ConfigureAwait(false);
                        return deleted > 0;
                    }
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while deleting ProductWeighing: {productWeighingId}", e.Message, productWeighingId);
            }

            return false;
        }

        private async Task<IEnumerable<ProductRecordEntity>> AddProductRecords(IEnumerable<ProductRecordEntity> productRecords)
        {
            try
            {
                await _context.ProductRecords.AddRangeAsync(productRecords).ConfigureAwait(false);
                _ = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return productRecords;
        }

        private IEnumerable<ProductRecordEntity> GetProductRecords(AddProductWeighingRequest productWeighing, int productWeighingId)
        {
            List<ProductRecordEntity> productRecords = new List<ProductRecordEntity>();

            foreach (var productPkg in productWeighing.ProductRecords)
            {
                double productGrossWeigh = productWeighing.GrossWeight / productPkg.Quantity;
                double productTareWeight = productWeighing.TareWeight / productPkg.Quantity;
                double productNetWeight = productWeighing.NetWeight / productPkg.Quantity;

                if (productPkg.Packaging.Type.Equals(Domain.Packagings.PackagingType.Caja))
                {
                    productRecords.Add(new ProductRecordEntity
                    {
                        ReferenceNumber = $"C{1:00}{productWeighingId:0000}",
                        ProductWeighingId = productWeighingId,
                        CampaingId = productWeighing.CampaingId,
                        ProductEntryNumber = productWeighing.ProductEntryNumber,
                        BuyerId = productWeighing.BuyerId,
                        SellerId = productWeighing.SellerId,
                        ColdRoomId = productWeighing.ColdRoomId,
                        FruitId = productWeighing.FruitId,
                        PackagingId = productPkg.Packaging.PackagingId,
                        IsOwnPackaging = productPkg.IsOwnPackaging,
                        GrossWeight = productGrossWeigh,
                        TareWeight = productTareWeight,
                        NetWeight = productNetWeight,
                        Quantity = productPkg.Quantity
                    });
                }
                else
                {
                    for (int i = 0; i < productPkg.Quantity; i++)
                    {
                        productRecords.Add(new ProductRecordEntity
                        {
                            ReferenceNumber = $"P{i + 1:00}{productWeighingId:0000}",
                            ProductWeighingId = productWeighingId,
                            CampaingId = productWeighing.CampaingId,
                            ProductEntryNumber = productWeighing.ProductEntryNumber,
                            BuyerId = productWeighing.BuyerId,
                            SellerId = productWeighing.SellerId,
                            ColdRoomId = productWeighing.ColdRoomId,
                            FruitId = productWeighing.FruitId,
                            PackagingId = productPkg.Packaging.PackagingId,
                            IsOwnPackaging = productPkg.IsOwnPackaging,
                            GrossWeight = productGrossWeigh,
                            TareWeight = productTareWeight,
                            NetWeight = productNetWeight,
                            Quantity = 1
                        });
                    }
                }

            }

            return productRecords;
        }

    }
}
