using AutoMapper;
using CustomerApi.Data.Persistence;
using CustomerApi.Service.Mappers;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace CustomerApi.Service.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public DbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public ILogger Logger { get; private set; }

        public QueryTestFixture()
        {
            Context = CustomerContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

            
        }

        public void Dispose()
        {
            CustomerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
