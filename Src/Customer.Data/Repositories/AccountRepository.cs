using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private const int CREDITLIMIT = 1000;

        private readonly Persistence.DbContext _accountDbContext = null;

        public AccountRepository(IOptions<Settings> settings)
        {
            _accountDbContext = new Persistence.DbContext(settings);
        }

        public async Task AddAccountAsync(Account account)
        {
           await _accountDbContext.Accounts.InsertOneAsync(account);
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _accountDbContext.Accounts.AsQueryable().FirstOrDefaultAsync(a => a.Email == email.ToLower());  
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _accountDbContext.Accounts.AsQueryable().ToListAsync();
        }
    }
}
