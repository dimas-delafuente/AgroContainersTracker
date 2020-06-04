using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class ColdRoomProfile : Profile
    {
        public ColdRoomProfile()
        {
            CreateMap<ColdRoomEntity, ColdRoom>().ReverseMap();
            CreateMap<AddColdRoomRequest, ColdRoomEntity>();
        }
    }
}