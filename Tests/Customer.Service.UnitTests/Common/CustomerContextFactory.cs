using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Service.UnitTests.Common
{
    public class CustomerContextFactory
    {
        public static CustomerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CustomerDbContext(options);

            context.Database.EnsureCreated();

            context.Customers.AddRange(new[] {
                new Customer("Test1","Test1@test.com.au",10000,1000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B05FB") },
                new Customer("Test2","Test2@test.com.au",10000,2000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B06FB") },
                new Customer("Test3","Test3@test.com.au",10000,10000){ Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B07FB") },
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(CustomerDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
