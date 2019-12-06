using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Data.Interfaces
{
    public interface ICustomerDbContext
    {
        DbSet<Customer> Customers { get; set; }

        DbSet<Account> Accounts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
