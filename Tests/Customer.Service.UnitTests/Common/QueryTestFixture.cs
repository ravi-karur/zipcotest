using AutoMapper;
using CustomerApi.Data.Persistence;
using CustomerApi.Service.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CustomerApi.Service.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public CustomerDbContext Context { get; private set; }
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
