using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CreditorProfile : Profile
    {
        public CreditorProfile()
        {
            CreateMap<AddCreditorRequest, CreditorEntity>();
            CreateMap<CreditorEntity, Creditor>();

            CreateMap<Creditor, CreditorEntity>()
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => GetCustomerCountry(src.Country)));
        }

        private int? GetCustomerCountry(Country country)
        {
            return country?.CountryId;
        }
    }
}