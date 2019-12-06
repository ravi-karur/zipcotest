using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

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
