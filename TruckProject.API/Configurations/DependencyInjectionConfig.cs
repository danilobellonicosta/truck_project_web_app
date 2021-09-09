using Microsoft.Extensions.DependencyInjection;
using TruckProject.API.Context;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Notifications;
using TruckProject.API.Repository;
using TruckProject.API.Services;

namespace TruckProject.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<MyContext>();
            services.AddScoped<ITruckServices, TruckServices>();
            services.AddScoped<IModelTruckServices, ModelTruckServices>();
            services.AddScoped<ITruckRepository, TruckRepository>();
            services.AddScoped<IModelTruckRepository, ModelTruckRepository>();
            services.AddScoped<INotifier, Notifier>();
        }
    }
}