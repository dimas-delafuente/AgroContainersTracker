using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllAsync();

        Task<Supplier> GetByIdAsync(int supplierId);

        Task AddAsync(AddSupplierRequest supplier);

        Task<bool> DeleteAsync(int supplierId);

        Task<bool> UpdateAsync(Supplier supplier);


    }
}
