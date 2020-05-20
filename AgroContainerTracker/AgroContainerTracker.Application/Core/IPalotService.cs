using AgroContainerTracker.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface IPalotService
    {
        Task<IEnumerable<Palot>> GetPalotsAsync();

        Task<Palot> GetPalotAsync(string id);

        Task AddPalotAsync(AddPalotRequest palot);
    }
}
