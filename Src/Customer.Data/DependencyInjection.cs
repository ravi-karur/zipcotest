using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomerDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<Persistence.DbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("CustomerDatabase")));

            //services.AddScoped<ICustomerDbContext>(provider => provider.GetService<Persistence.DbContext>());

            return services;
        }
    }
}
