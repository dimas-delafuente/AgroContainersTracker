using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgroContainerTracker.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public SupplierService()
        {
        }

        public SupplierService(ApplicationContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        public async Task AddAsync(AddSupplierRequest supplier)
        {
            try
            {
                if (supplier == null)
                    throw new ArgumentNullException();

                SupplierEntity entity = _mapper.Map<SupplierEntity>(supplier);

                var addResponse = await _context.Suppliers.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                    await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                _context.DetachAll();
                throw;
            }
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            IEnumerable<SupplierEntity> entities = await _context.Suppliers.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<Supplier>>(entities);
        }

        public async Task<Supplier> GetByIdAsync(int supplierId)
        {
            if (supplierId < 0)
                throw new ArgumentOutOfRangeException();

            SupplierEntity entity = await _context.Suppliers
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.SupplierId.Equals(supplierId))
                .ConfigureAwait(false);

            return _mapper.Map<Supplier>(entity);
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
            catch (Exception)
            {
                _context.DetachAll();
                return false;
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

                return false;
            }
            catch (Exception)
            {
                _context.DetachAll();
                throw;
            }
        }
    }
}
