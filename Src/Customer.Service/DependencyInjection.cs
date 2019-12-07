using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CustomerApi.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomerServicesDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
