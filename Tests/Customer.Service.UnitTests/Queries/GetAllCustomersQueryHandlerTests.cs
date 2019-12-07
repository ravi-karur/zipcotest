using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Models;
using CustomerApi.Domain.Queries;
using CustomerApi.Service.Handlers.Query;
using CustomerApi.Service.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Service.UnitTests.Queries
{
    [Collection("QueryCollection")]
    public class GetAllCustomersQueryHandlerTests
    {
        private readonly DbContext _context;
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
            var mockedCustomer1 = new Customer("test1", "test1@test.com.au", 10000, 1000);
            var mockedCustomer2 = new Customer("test2", "test2@test.com.au", 20000, 2000);

            var customerList = new List<Customer>() { mockedCustomer1, mockedCustomer2 };

            var mock = new Mock<ICustomerRepository>();
            mock.Setup(srv => srv.GetAllCustomers()).ReturnsAsync(customerList);

            var sut = new GetAllCustomersQueryHandler(_logger, _mapper, mock.Object);

            var result = await sut.Handle(new GetAllCustomersQuery(), CancellationToken.None);

            var sortedResult = result.OrderBy(x => x.Name).ToList();

            
            Assert.NotNull(sortedResult);
            Assert.Equal(2, sortedResult.Count);
            Assert.Collection(sortedResult,
                    item =>
                    {
                        Assert.Equal("test1", item.Name);
                        Assert.Equal("test1@test.com.au", item.Email);
                    },
                    item =>
                    {
                        Assert.Equal("test2", item.Name);
                        Assert.Equal("test2@test.com.au", item.Email);
                    });

            mock.Verify(rep => rep.GetAllCustomers(), Times.Once);

        }
    }
}
