using System;
using System.Linq;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Packagings;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class PackagingProfile : Profile
    {
        public PackagingProfile()
        {
            CreateMap<PackagingEntity, Packaging>()
                .ForMember(dest => dest.PackagingMovements, opt => opt.MapFrom(src => src.PackagingMovements.OrderByDescending(x => x.Created)));

            CreateMap<AddPackagingRequest, PackagingEntity>();
            CreateMap<Packaging, PackagingEntity>()
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.PackagingMovements, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Owner.CustomerId));

        }
    }

    public class PackagingMovementProfile : Profile
    {
        public PackagingMovementProfile()
        {
            CreateMap<PackagingMovementEntity, PackagingMovement>().ReverseMap();
            CreateMap<AddPackagingMovementRequest, PackagingMovementEntity>();
        }
    }
}
