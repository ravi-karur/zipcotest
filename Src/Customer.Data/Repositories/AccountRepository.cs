using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private const int CREDITLIMIT = 1000;

        private ICustomerDbContext _customerDbContext;
        
        public AccountRepository(CustomerDbContext dbContext) : base(dbContext)
        {
            _customerDbContext = dbContext;
        }

        public Account GetAccountByCustomerId(Guid customerId)
        {
            return _customerDbContext.Accounts.Where(a => a.CustomerId == customerId).FirstOrDefault();
        }

        public Account GetAccountByEmail(string email)
        {
            return _customerDbContext.Accounts.Where(a => a.Email == email).FirstOrDefault();
        }

        public bool IsCustomerEligibleForAccount(Customer customer)
        {
            
            return (customer.MonthlyIncome - customer.MonthlyExpense) >= CREDITLIMIT ? true : false;
        }
    }
}
