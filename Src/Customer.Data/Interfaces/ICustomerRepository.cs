using CustomerApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> EmailExistAsync(string email);

        public Customer GetCustomerByEmail(string email);
    }
}
