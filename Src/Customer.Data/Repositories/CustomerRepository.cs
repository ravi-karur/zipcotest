using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private const int CREDITLIMIT = 1000;

        private readonly DbContext _customerDbContext = null;

        public CustomerRepository(IOptions<Settings> settings)
        {
            _customerDbContext = new DbContext(settings);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerDbContext.Customers.InsertOneAsync(customer);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerDbContext.Customers.AsQueryable().ToListAsync();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _customerDbContext.Customers.AsQueryable().FirstOrDefaultAsync(customer => customer.Email == email.ToLower());
        }

        public bool IsCustomerEligibleForAccount(Customer customer)
        {
            return (customer.MonthlyIncome - customer.MonthlyExpense) >= CREDITLIMIT ? true : false;
        }
    }
}
