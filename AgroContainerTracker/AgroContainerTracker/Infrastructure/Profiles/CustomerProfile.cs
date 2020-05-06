using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain.Rates;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<AddCustomerRequest, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();

            CreateMap<Customer, CustomerEntity>()
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => GetCustomerCountry(src.Country)))
                .ForMember(dest => dest.RateId, opt => opt.MapFrom(src => GetRateId(src.Rate)));

            CreateMap<CustomerEntity, CustomerDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.CustomerNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }

        private int? GetCustomerCountry(Country country)
        {
            return country?.CountryId;
        }

        private int? GetRateId(Rate rate)
        {
            return rate?.RateId;
        }
    }
}
