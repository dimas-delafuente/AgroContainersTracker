using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ILogger<SupplierService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public SupplierService(ApplicationContext context, IMapper mapper, ILogger<SupplierService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Supplier> AddAsync(AddSupplierRequest supplier)
        {
            try
            {
                if (supplier == null)
                    throw new ArgumentNullException();

                SupplierEntity entity = _mapper.Map<SupplierEntity>(supplier);

                var addResponse = await _context.Suppliers.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Supplier>(addResponse.Entity) : null;
                }
                    
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Supplier: {supplier}", e.Message, JsonSerializer.Serialize(supplier));
            }

            return null;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            try
            {
                IEnumerable<SupplierEntity> entities = await _context.Suppliers
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Supplier>>(entities);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all Suppliers", e.Message);
            }

            return new List<Supplier>();
        }

        public async Task<Supplier> GetByIdAsync(int supplierId)
        {
            try
            {
                if (supplierId < 0)
                    throw new ArgumentOutOfRangeException();

                SupplierEntity entity = await _context.Suppliers
                    .AsNoTracking()
                    .Include(x => x.Country)
                    .FirstOrDefaultAsync(x => x.SupplierId.Equals(supplierId))
                    .ConfigureAwait(false);

                return _mapper.Map<Supplier>(entity);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Supplier: {supplierId}", e.Message, supplierId);
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int supplierId)
        {
            try
            {
                SupplierEntity supplier = await _context.Suppliers.FindAsync(supplierId).ConfigureAwait(false);

                if (supplier != null)
                {
                    var removedEntity = _context.Suppliers.Remove(supplier);
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
                _logger.LogError(e, "Exception: {e} // Internal Error while removing Supplier: {supplierId}", e.Message, supplierId);
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            try
            {
                if (supplier == null)
                    throw new ArgumentNullException();

                SupplierEntity entity = await _context.Suppliers
                    .FindAsync(supplier.SupplierId)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(supplier, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }
            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Supplier: {supplier}", e.Message, JsonSerializer.Serialize(supplier));
            }

            return false;
        }
    }
}
