using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomerDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CustomerDatabase")));

            services.AddScoped<ICustomerDbContext>(provider => provider.GetService<CustomerDbContext>());

            return services;
        }
    }
}
