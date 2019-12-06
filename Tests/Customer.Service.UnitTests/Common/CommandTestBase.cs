using System;
using CustomerApi.Data.Persistence;

namespace CustomerApi.Service.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly CustomerDbContext _context;

        public CommandTestBase()
        {
            _context = CustomerContextFactory.Create();
        }

        public void Dispose()
        {
            CustomerContextFactory.Destroy(_context);
        }
    }
}