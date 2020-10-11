using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain;
using AgroContainerTracker.Domain.Companies;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class CarrierProfile : Profile
    {
        public CarrierProfile()
        {
            CreateMap<AddCarrierRequest, CarrierEntity>()
                .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles))
                .ForMember(dest => dest.Carriages, opt => opt.MapFrom(src => src.Carriages))
                .ForMember(dest => dest.Drivers, opt => opt.MapFrom(src => src.Drivers));

            CreateMap<CarrierEntity, Carrier>();

            CreateMap<Carrier, CarrierEntity>()
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => GetCustomerCountry(src.Country)));
        }

        private int? GetCustomerCountry(Country country)
        {
            return country?.CountryId;
        }
    }

    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleEntity, Vehicle>().ReverseMap();
        }
    }

    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverEntity, Driver>().ReverseMap();
        }
    }

    public class CarriageProfile : Profile
    {
        public CarriageProfile()
        {
            CreateMap<CarriageEntity, Carriage>().ReverseMap();
        }
    }
}
