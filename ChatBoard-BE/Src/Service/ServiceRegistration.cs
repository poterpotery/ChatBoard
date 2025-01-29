using Microsoft.Extensions.DependencyInjection;
using Service.Implementations;
using Service.Interfaces;
using Repository;
using Service.Implementations.Unit;
using Service.Interfaces.Unit;

namespace Service
{
    public static class ServiceRegistration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddRepository();
            services.AddScoped<IServiceUnit, ServiceUnit>();
            services.AddScoped<IFileManagementService, FileManagementService>();
        }
    }
}