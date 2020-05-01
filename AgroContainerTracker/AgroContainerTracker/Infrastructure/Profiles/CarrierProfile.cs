using AgroContainerTracker.Data.Entities;
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
                .ForMember(dest => dest.Drivers, opt => opt.MapFrom(src => src.Drivers));

            CreateMap<CarrierEntity, Carrier>();
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
}
