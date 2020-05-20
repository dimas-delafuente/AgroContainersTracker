using AgroContainerTracker.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface IContainerService
    {
        Task<IEnumerable<Container>> GetContainersAsync();

        Task<Container> GetContainerAsync(int id);

        Task AddContainerAsync(AddContainerRequest container);

    }
}
