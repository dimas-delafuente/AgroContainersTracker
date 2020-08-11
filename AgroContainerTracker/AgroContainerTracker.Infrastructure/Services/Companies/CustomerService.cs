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
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationContext context, IMapper mapper, ILogger<CustomerService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Customer> AddAsync(AddCustomerRequest customer)
        {  
            try
            {
                if (customer == null)
                    throw new ArgumentNullException();

                CustomerEntity entity = _mapper.Map<CustomerEntity>(customer);
                var addResponse = await _context.Customers.AddAsync(entity).ConfigureAwait(false);

                if (addResponse.State.Equals(EntityState.Added))
                {
                    bool created = await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
                    return created ? _mapper.Map<Customer>(addResponse.Entity) : null;
                }
                    
            }
            catch(Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while adding new Customer: {customer}", e.Message, JsonSerializer.Serialize(customer));
            }

            return null;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                IEnumerable<CustomerEntity> entities = await _context.Customers
                .AsNoTracking()
                .ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<Customer>>(entities);

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving all Customers.", e.Message);

            }
            return new List<Customer>();
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            try
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

            } catch (Exception e)
            {
                _logger.LogError(e, "Exception: {e} // Internal Error while retrieving Customer: {customerId}", e.Message, customerId);
            }

            return null;
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
                _logger.LogError(e, "Exception: {e} // Internal Error while removing Customer: {customerId}", e.Message, customerId);
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

            }
            catch (Exception e)
            {
                _context.DetachAll();
                _logger.LogError(e, "Exception: {e} // Internal Error while updating Customer: {customer}", e.Message, JsonSerializer.Serialize(customer));
            }

            return false;
        }
    }
}
