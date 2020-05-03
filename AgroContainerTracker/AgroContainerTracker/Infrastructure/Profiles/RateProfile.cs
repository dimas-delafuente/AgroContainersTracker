using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Rates;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class RateProfile : Profile
    {
        public RateProfile()
        {
            CreateMap<RateEntity, Rate>().ReverseMap();
            CreateMap<AddRateRequest, RateEntity>();

        }
    }
}