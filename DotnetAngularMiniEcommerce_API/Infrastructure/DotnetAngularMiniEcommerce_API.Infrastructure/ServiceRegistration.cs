using DotnetAngularMiniEcommerce_API.Application.Abstractions.Storage;
using DotnetAngularMiniEcommerce_API.Application.Abstractions.Token;
using DotnetAngularMiniEcommerce_API.Infrastructure.Enums;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage.Azure;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage.Local;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetAngularMiniEcommerce_API.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType) {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
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
