using CustomerApi.Data.Persistence;
using System;

namespace CustomerApi.Service.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly DbContext _context;

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