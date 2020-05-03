using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer> GetByIdAsync(int customerId);

        Task AddAsync(AddCustomerRequest customer);

        Task<bool> DeleteAsync(int customerId);

        Task<bool> UpdateAsync(Customer customer);
    }
}
