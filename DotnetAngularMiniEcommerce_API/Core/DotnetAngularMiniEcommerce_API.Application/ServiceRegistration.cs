using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetAngularMiniEcommerce_API.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services) 
        {
            services.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
