using AgroContainerTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroContainerTracker.Services
{
    public interface IPalotService
    {
        Task<IEnumerable<Palot>> GetPalotsAsync();

        Task<Palot> GetPalotAsync(string id);

        Task AddPalotAsync(AddPalotRequest palot);
    }
}
