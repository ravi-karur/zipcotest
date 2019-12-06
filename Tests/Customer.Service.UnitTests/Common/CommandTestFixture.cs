using AutoMapper;
using CustomerApi.Data.Persistence;
using CustomerApi.Service.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CustomerApi.Service.UnitTests.Common
{
    public class CommandTestFixture : IDisposable
    {
        public CustomerDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public ILogger Logger { get; private set; }

        public CommandTestFixture()
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

    [CollectionDefinition("CommandCollection")]
    public class CommandCollection : ICollectionFixture<CommandTestFixture> { }
}
