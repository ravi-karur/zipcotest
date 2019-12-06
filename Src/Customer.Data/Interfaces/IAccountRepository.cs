using CustomerApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account GetAccountByCustomerId(Guid customerId);
        public Account GetAccountByEmail(string email);

        public bool IsCustomerEligibleForAccount(Customer customer);
    }
}
