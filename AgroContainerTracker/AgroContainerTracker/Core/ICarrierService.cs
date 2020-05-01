using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain.Companies;

namespace AgroContainerTracker.Core.Services
{
    public interface ICarrierService
    {
        Task<IEnumerable<Carrier>> GetAllAsync();
        
        Task<Carrier> GetByIdAsync(int id);

        Task AddAsync(AddCarrierRequest company);
    }
}
