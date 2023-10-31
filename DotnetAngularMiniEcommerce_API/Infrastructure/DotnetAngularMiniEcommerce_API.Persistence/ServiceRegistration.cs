using DotnetAngularMiniEcommerce_API.Application.Abstractions;
using DotnetAngularMiniEcommerce_API.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetAngularMiniEcommerce_API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>();
        }
    }
}
