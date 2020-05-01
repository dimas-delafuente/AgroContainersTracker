using System;
using System.Collections.Generic;
using System.Linq;
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
            catch (Exception e)
            {

            }
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            IEnumerable<SupplierEntity> entities = await _context.Suppliers.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Supplier>>(entities);
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();

            SupplierEntity entity = await _context.Suppliers
                .Where(x => x.CompanyId.Equals(id))
                .Include(x => x.Country)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return _mapper.Map<Supplier>(entity);
        }
    }
}
