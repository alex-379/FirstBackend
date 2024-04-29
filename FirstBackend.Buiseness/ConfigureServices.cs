using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FirstBackend.Buiseness
{
    public static class ConfigureServices
    {
        public static void ConfigureBllServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<IPasswordsService, PasswordsService>();
        }
    }
}
