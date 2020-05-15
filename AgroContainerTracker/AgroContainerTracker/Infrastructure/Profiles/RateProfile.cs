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

            CreateMap<RateEntity, RateDetails>()
                .ForMember(src => src.Customers, opt => opt.MapFrom(src => src.Customers));

        }
    }
}