using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.ProductManagement;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CampaingProfile : Profile
    {
        public CampaingProfile()
        {
            CreateMap<Campaing, CampaingEntity>().ReverseMap();
        }
    }
}
