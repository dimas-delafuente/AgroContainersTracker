using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryEntity, Country>().ReverseMap();
        }
    }
}
