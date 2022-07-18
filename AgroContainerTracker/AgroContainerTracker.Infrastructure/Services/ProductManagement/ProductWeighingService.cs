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
    public class WeighingService : IWeighingService
    {
        private readonly ILogger<WeighingService> _logger;
        private readonly ApplicationContext _context;
        private readonly ICampaignService _CampaignService;
        private readonly IMapper _mapper;

        public WeighingService(ApplicationContext context, ICampaignService CampaignService, IMapper mapper, ILogger<WeighingService> logger)
        {
            _context = context;
            _CampaignService = CampaignService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Weighing> AddAsync(AddWeighingRequest Weighing)
        {
            if (Weighing == null || !(Weighing.ProductRecords?.Count > 0))
                throw new ArgumentNullException();

            try
            {
                
                WeighingEntity entity = _mapper.Map<WeighingEntity>(Weighing);
                entity.WeighingId = await GetCampaignNextWeighingId(entity.CampaignId);

                var addResponse = await _context.Weighings.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;

                    if (created)
                    {
                        var productRecords = GetProductRecords(Weighing, addResponse.Entity.WeighingId);
                        addResponse.Entity.ProductRecords = (ICollection<ProductRecordEntity>)await AddProductRecords(productRecords);
                    }

                    return created ? await GetByIdAsync(addResponse.Entity.WeighingId) : null;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Product Weighting: {Weighing}", e.Message, JsonSerializer.Serialize(Weighing));
            }

            return null;
        }

        public async Task<Weighing> UpdateAsync(AddWeighingRequest Weighing)
        {
            if (Weighing == null || !(Weighing.ProductRecords?.Count > 0))
                throw new ArgumentNullException();

            try
            {

                WeighingEntity entity = await _context.Weighings
                    .Include(x => x.ProductRecords)
                    .FirstOrDefaultAsync(x => x.WeighingId.Equals(Weighing.WeighingId))
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(Weighing, entity);
                    entity.ProductRecords = (ICollection<ProductRecordEntity>)GetProductRecords(Weighing, entity.WeighingId);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    return await GetByIdAsync(entity.WeighingId);
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Product Weighting: {Weighing}", e.Message, JsonSerializer.Serialize(Weighing));
            }

            return null;
        }

        public async Task<List<Weighing>> GetAllAsync(int CampaignId, int InputNumber)
        {
            try
            {
                if (CampaignId < 0)
                    throw new ArgumentOutOfRangeException();

                IEnumerable<WeighingEntity> entities = await _context.Weighings
                    .AsNoTracking()
                    .Include(x => x.Input)
                    .Include(x => x.ProductRecords)
                        .ThenInclude(x => x.Packaging)
                    .Include(x => x.Storage)
                    .Include(x => x.Fruit)
                    .Include(x => x.Buyer)
                    .Include(x => x.Seller)
                    .Where(x => x.CampaignId.Equals(CampaignId) && x.InputNumber.Equals(InputNumber))
                    .ToListAsync()
                    .ConfigureAwait(false);

                return _mapper.Map<List<Weighing>>(entities);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all Product Weightings. CampaignId:{CampaignId}, InputNumber:{InputNumber}",
                                    e.Message, CampaignId, InputNumber);
            }

            return new List<Weighing>();
        }

        public async Task<Weighing> GetByIdAsync(int WeighingId)
        {
            try
            {
                if (WeighingId < 0)
                    throw new ArgumentOutOfRangeException();

                WeighingEntity entities = await _context.Weighings
                    .AsNoTracking()
                    .Include(x => x.ProductRecords)
                        .ThenInclude(p => p.Packaging)
                    .Include(x => x.Buyer)
                    .Include(x => x.Seller)
                    .Include(x => x.Fruit)
                    .Include(x => x.Storage)
                    .Include(x => x.Rate)
                    .FirstOrDefaultAsync(x => x.WeighingId.Equals(WeighingId))
                    .ConfigureAwait(false);

                return _mapper.Map<Weighing>(entities);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Product Weightings. WeighingId:{WeighingId}.}",
                                    e.Message, WeighingId);
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int WeighingId)
        {
            try
            {
                WeighingEntity Weighing = await _context.Weighings.FindAsync(WeighingId).ConfigureAwait(false);

                if (Weighing != null)
                {
                    var removedEntity = _context.Weighings.Remove(Weighing);
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
                _logger.LogError(e, "Exception: {e} // Internal Error while deleting Weighing: {WeighingId}", e.Message, WeighingId);
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

        private IEnumerable<ProductRecordEntity> GetProductRecords(AddWeighingRequest Weighing, int WeighingId)
        {
            List<ProductRecordEntity> productRecords = new List<ProductRecordEntity>();

            foreach (var productPkg in Weighing.ProductRecords)
            {
                double productGrossWeigh = Weighing.GrossWeight / productPkg.Quantity;
                double productTareWeight = Weighing.TareWeight / productPkg.Quantity;
                double productNetWeight = Weighing.NetWeight / productPkg.Quantity;

                if (productPkg.Packaging.Type.Equals(Domain.Packagings.PackagingType.Caja))
                {
                    productRecords.Add(new ProductRecordEntity
                    {
                        WeighingId = WeighingId,
                        CampaignId = Weighing.CampaignId,
                        InputNumber = Weighing.InputNumber,
                        BuyerId = Weighing.BuyerId,
                        SellerId = Weighing.SellerId,
                        StorageId = Weighing.StorageId,
                        FruitId = Weighing.FruitId,
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
                    char packagingTypeLabel = (char) productPkg.Packaging.Type;
                    for (int i = 0; i < productPkg.Quantity; i++)
                    {
                        productRecords.Add(new ProductRecordEntity
                        {
                            ReferenceNumber = $"{packagingTypeLabel}{i + 1:00}{WeighingId:0000}",
                            WeighingId = WeighingId,
                            CampaignId = Weighing.CampaignId,
                            InputNumber = Weighing.InputNumber,
                            BuyerId = Weighing.BuyerId,
                            SellerId = Weighing.SellerId,
                            StorageId = Weighing.StorageId,
                            FruitId = Weighing.FruitId,
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

        private async Task<int> GetCampaignNextWeighingId(int CampaignId)
        {
            return await _CampaignService.GetCampaignNextWeightingId(CampaignId).ConfigureAwait(false);
        }
    }
}
