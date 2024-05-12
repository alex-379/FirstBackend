using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FirstBackend.Business.Configuration
{
    public static class ConfigureServices
    {
        public static void ConfigureBllServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<IPasswordsService, PasswordsService>();
            services.AddScoped<ITokensService, TokensService>();
        }
    }
}