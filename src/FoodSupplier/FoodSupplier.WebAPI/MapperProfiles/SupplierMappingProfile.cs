using AutoMapper;
using FoodSupplier.BusinessLogic.Models;
using FoodSupplier.DataAccess.Entities;
using FoodSupplier.WebAPI.Models;

namespace FoodSupplier.WebAPI.MapperProfiles;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<Shop, ShopModel>().ReverseMap();
        CreateMap<ShopCreateModel, Shop>();

        CreateMap<Shop, ShopEntity>().ReverseMap();
    }
}