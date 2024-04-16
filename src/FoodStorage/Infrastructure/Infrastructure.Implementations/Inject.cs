﻿using FoodStorage.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStorage.Infrastructure.Implementations;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(o => o.UseNpgsql(configuration["DbConnection"]));
        return services;
    }
}