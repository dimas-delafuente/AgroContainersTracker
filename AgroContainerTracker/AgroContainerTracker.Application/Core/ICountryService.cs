using System.Collections.Generic;
using System.Threading.Tasks;
using AgroContainerTracker.Domain;

namespace AgroContainerTracker.Core.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAll();
    }
}
