using DotnetAngularMiniEcommerce_API.Application.Services;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetAngularMiniEcommerce_API.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
