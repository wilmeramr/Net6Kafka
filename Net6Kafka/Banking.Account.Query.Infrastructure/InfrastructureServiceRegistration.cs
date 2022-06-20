﻿using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Infrastructure.Persistence;
using Banking.Account.Query.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Account.Query.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInsfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<MySqlDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            return services;
        }
    }
}
