using AutoMapper;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Queries;
using CustomerApi.Service.Handlers.Query;
using CustomerApi.Service.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Service.UnitTests.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerQueryHandlerTests
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCustomerQueryHandler> _logger;


        public GetCustomerQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _logger = Mock.Of<ILogger<GetCustomerQueryHandler>>();

        }

        [Fact]
        public async Task GetCustomerDetailExists()
        {
            var sut = new GetCustomerQueryHandler(_logger,_mapper,_context);

            var result = await sut.Handle(new GetCustomerQuery { CustomerId = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B05FB") }, CancellationToken.None);

            Assert.IsType<CustomerDto>(result);
            Assert.Equal(Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B05FB"), result.Id);
            Assert.Equal("Test1", result.Name);
            Assert.Equal("Test1@test.com.au", result.Email);
            Assert.Equal(10000M, result.MonthlyIncome);
            Assert.Equal(1000M, result.MonthlyExpense);

        }

        [Fact]
        public async Task GetCustomerDetailNotFound()
        {
            var sut = new GetCustomerQueryHandler(_logger, _mapper, _context);

            var exceptionResult = await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new GetCustomerQuery { CustomerId = Guid.Parse("ACA5A84B-CD2C-441B-9795-632FBC0B05FB") }, CancellationToken.None));

            Assert.Contains("not found", exceptionResult.Message);

        }
    }
}
