using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FoodUserAuth.WebApi.Extensions
{
    public static class MapperServiceCollectionExtentsion
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services.AddScoped<IMapper>(_ => CreateDefaultMapper());
        }

        public static IMapper CreateDefaultMapper()
        {
            throw new NotImplementedException();
        }
    }
}
