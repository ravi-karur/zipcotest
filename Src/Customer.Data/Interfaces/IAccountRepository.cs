using CustomerApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Data.Interfaces
{
    public interface IAccountRepository 
    {
        public Task AddAccountAsync(Account customer);
        
        public Task<List<Account>> GetAllAccounts();

        public Task<Account> GetAccountByEmail(string email);

    }
}
