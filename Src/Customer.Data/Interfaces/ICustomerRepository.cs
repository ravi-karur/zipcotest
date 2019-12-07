using CustomerApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Data.Interfaces
{
    public interface ICustomerRepository 
    {
        public Task AddCustomerAsync(Customer customer); 
        public Task<Customer> GetCustomerByEmail(string email);

        public Task<List<Customer>> GetAllCustomers();

        public bool IsCustomerEligibleForAccount(Customer customer);
    }
}
