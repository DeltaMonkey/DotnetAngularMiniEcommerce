using DotnetAngularMiniEcommerce_API.Application.Abstractions.Storage;
using DotnetAngularMiniEcommerce_API.Infrastructure.Enums;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetAngularMiniEcommerce_API.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType) {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.AWS:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
