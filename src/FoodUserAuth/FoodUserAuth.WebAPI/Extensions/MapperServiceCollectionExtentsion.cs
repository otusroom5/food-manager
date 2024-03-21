using AutoMapper;
using System.Runtime.InteropServices;

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
            var mapperConfiguration = new MapperConfiguration(cfg => {

            });

            return new Mapper(mapperConfiguration);
        }
    }
}
