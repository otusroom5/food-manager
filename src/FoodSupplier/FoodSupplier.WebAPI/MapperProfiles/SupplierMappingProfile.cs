using AutoMapper;
using FoodSupplier.BusinessLogic.Models;
using FoodSupplier.DataAccess.Entities;
using FoodSupplier.WebAPI.Models;

namespace FoodSupplier.WebAPI.MapperProfiles;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        //webAPI maps
        CreateMap<Shop, ShopModel>().ReverseMap();
        CreateMap<PriceEntry, PriceModel>();
        CreateMap<ShopCreateModel, Shop>();

        //BL maps
        CreateMap<Shop, ShopEntity>().ReverseMap();
        CreateMap<PriceEntry, PriceEntryEntity>().ReverseMap();
    }
}