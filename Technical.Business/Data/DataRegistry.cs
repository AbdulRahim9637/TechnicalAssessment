using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Technical.Business.Data.Repository;

namespace Technical.Business.Data
{
    public static class DataRegistry
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IBeerRespository, BeerRespository>();
            services.AddScoped<IBarRespository, BarRespository>();
            services.AddScoped<IBreweryBeersRepsoitory,BreweryBeersRepsoitory>();
            services.AddScoped<IBarBeersRepsoitory, BarBeersRespository>();
            return services;
        }
    }

}