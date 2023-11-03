using DotnetAngularMiniEcommerce_API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetAngularMiniEcommerce_API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceDbContext>(options => {
                options.UseNpgsql(Configuration.ConnectionString);
            });
        }
    }
}
