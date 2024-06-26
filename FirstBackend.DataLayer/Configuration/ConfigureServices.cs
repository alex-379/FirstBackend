﻿using FirstBackend.DataLayer.Interfaces;
using FirstBackend.DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FirstBackend.DataLayer.Configuration
{
    public static class ConfigureServices
    {
        public static void ConfigureDalServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IDevicesRepository, DevicesRepository>();
            services.AddScoped<ISaltsRepository, SaltsRepository>();
            services.AddScoped(typeof(ITransactionsRepository<>), typeof(TransactionsRepository<>));
        }
    }
}
