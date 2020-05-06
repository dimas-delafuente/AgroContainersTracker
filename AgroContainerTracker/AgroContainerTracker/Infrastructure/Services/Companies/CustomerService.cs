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
                _context.DetachAll();
                throw;
            }
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            IEnumerable<CustomerEntity> entities = await _context.Customers
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);
            return _mapper.Map<List<Customer>>(entities);
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            if (customerId < 0)
                throw new ArgumentOutOfRangeException();

            CustomerEntity entity = await _context.Customers
                .AsNoTracking()
                .Include(x => x.Country)
                .Include(x => x.Rate)
                .FirstOrDefaultAsync(x => x.CustomerId.Equals(customerId))
                .ConfigureAwait(false);

            return _mapper.Map<Customer>(entity);
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            try
            {
                CustomerEntity customer = await _context.Customers.FindAsync(customerId).ConfigureAwait(false);

                if (customer != null)
                {
                    var removedEntity = _context.Customers.Remove(customer);
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
                return false;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            CustomerEntity entity = null;
            try
            {
                if (customer == null)
                    throw new ArgumentNullException();

                entity = await _context.Customers
                    .FindAsync(customer.CustomerId)
                    .ConfigureAwait(false);

                if (entity != null)
                {
                    _mapper.Map(customer, entity);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                _context.DetachAll();
                throw;
            }
        }
    }
}
