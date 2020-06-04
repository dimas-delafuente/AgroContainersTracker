using AgroContainerTracker.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroContainerTracker.Core.Services
{
    public interface IColdRoomService
    {
        Task<List<ColdRoom>> GetAllAsync();

        Task<ColdRoom> GetByIdAsync(int coldRoomId);

        Task<ColdRoom> AddAsync(AddColdRoomRequest coldRoom);

        Task<bool> DeleteAsync(int coldRoomId);
        Task<bool> UpdateAsync(ColdRoom coldRoom);

        Task<bool> ExistsAsync(int coldRoomNumber);
    }
}
