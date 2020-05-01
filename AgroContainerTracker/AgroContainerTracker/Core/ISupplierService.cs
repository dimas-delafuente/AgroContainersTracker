using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync();

        Task<Supplier> GetByIdAsync(int id);

        Task AddAsync(AddSupplierRequest supplier);
    }
}
