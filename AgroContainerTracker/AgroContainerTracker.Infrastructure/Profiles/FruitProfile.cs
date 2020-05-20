using System;
using AgroContainerTracker.Data.Entities;
using AgroContainerTracker.Domain.Fruits;
using AutoMapper;

namespace AgroContainerTracker.Infrastructure.Profiles
{
    public class FruitProfile : Profile
    {
        public FruitProfile()
        {
            CreateMap<FruitEntity, Fruit>().ReverseMap();
            CreateMap<AddFruitRequest, FruitEntity>();

        }
    }
}
