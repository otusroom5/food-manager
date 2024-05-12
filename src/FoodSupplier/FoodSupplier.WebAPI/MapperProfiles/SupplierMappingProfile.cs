using AutoMapper;
using FoodSupplier.BusinessLogic.Dto;
using FoodSupplier.WebAPI.Models;

namespace FoodSupplier.WebAPI.MapperProfiles;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<ShopDto, ShopModel>().ReverseMap();
        CreateMap<ShopCreateModel, ShopDto>();
    }
}