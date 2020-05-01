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
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(AddCustomerRequest customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException();

                CustomerEntity entity = _mapper.Map<CustomerEntity>(customer);

                var addResponse = await _context.Customers.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                    await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch(Exception e)
            {

            }
            

        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            IEnumerable<CustomerEntity> entities = await _context.Customers.AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Customer>>(entities);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();

            CustomerEntity entity = await _context.Customers
                .Where(x => x.CompanyId.Equals(id))
                .Include(x => x.Country)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return _mapper.Map<Customer>(entity);

        }
    }
}
