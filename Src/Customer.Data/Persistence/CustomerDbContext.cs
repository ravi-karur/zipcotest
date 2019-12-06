using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Data.Persistence
{
    public class CustomerDbContext : DbContext, ICustomerDbContext
    {

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {   
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
        }

    }
}
