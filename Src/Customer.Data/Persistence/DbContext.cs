using CustomerApi.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomerApi.Data.Persistence
{
    public class DbContext
    {
        private readonly IMongoDatabase _database = null;

        public DbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Customer> Customers
        {
            get
            {
                return _database.GetCollection<Customer>("Customers");
            }
        }

        public IMongoCollection<Account> Accounts
        {
            get
            {
                return _database.GetCollection<Account>("Accounts");
            }
        }
    }
}
