using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer> GetByIdAsync(int id);

        Task AddAsync(AddCustomerRequest company);
    }
}
