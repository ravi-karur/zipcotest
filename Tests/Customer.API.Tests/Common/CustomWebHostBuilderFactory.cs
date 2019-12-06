using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerService;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CustomerApi.API.Tests.Common
{
    public class CustomWebHostBuilderFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = base.CreateWebHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Remove other loggers                
            });

            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureTestServices((services) =>
                {
                    services.RemoveAll(typeof(IHostedService));
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    // Add a database context using an in-memory 
                    // database for testing.
                    services.AddDbContext<CustomerDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTestin");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    //services.AddControllers();
                    //services.AddMediatR();
                    //services.AddLogging();

                    services.AddScoped<ICustomerDbContext>(provider => provider.GetService<CustomerDbContext>());
                    //services.UseTestServer();

                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<CustomerDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebHostBuilderFactory<TStartup>>>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        //Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database with test messages. Error: {ex.Message}");
                    }
                })
                .UseEnvironment("Test");
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

    }
}
