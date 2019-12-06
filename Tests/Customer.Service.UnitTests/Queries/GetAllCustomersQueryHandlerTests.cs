using AutoMapper;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Queries;
using CustomerApi.Service.Handlers.Query;
using CustomerApi.Service.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Service.UnitTests.Queries
{
    [Collection("QueryCollection")]
    public class GetAllCustomersQueryHandlerTests
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCustomersQueryHandler> _logger;


        public GetAllCustomersQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _logger = Mock.Of<ILogger<GetAllCustomersQueryHandler>>();

        }

        [Fact]
        public async Task GetAllCustomerDetailTests()
        {
            var sut = new GetAllCustomersQueryHandler(_logger, _mapper, _context);

            var result = await sut.Handle(null, CancellationToken.None);

            var sortedResult = result.OrderBy(x => x.Name).ToList();

            //foreach ( var )
            
            Assert.NotNull(sortedResult);
            Assert.Equal(3, sortedResult.Count);
            Assert.Collection(sortedResult,
                    item =>
                    {
                        Assert.Equal("Test1", item.Name);
                        Assert.Equal("Test1@test.com.au", item.Email);
                    },
                    item =>
                    {
                        Assert.Equal("Test2", item.Name);
                        Assert.Equal("Test2@test.com.au", item.Email);
                    },
                    item =>
                    {
                        Assert.Equal("Test3", item.Name);
                        Assert.Equal("Test3@test.com.au", item.Email);
                    });
            

        }
    }
}
