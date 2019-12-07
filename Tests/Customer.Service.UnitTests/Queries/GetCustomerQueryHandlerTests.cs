using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using CustomerApi.Domain.Queries;
using CustomerApi.Service.Handlers.Query;
using CustomerApi.Service.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Service.UnitTests.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerQueryHandlerTests
    {
        private readonly DbContext _context;
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
            var mockedCustomer = new Customer("test1","test@test.com.au",10000,1000);
            
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(mockedCustomer);

            var sut = new GetCustomerQueryHandler(_logger, _mapper, mock.Object);

            var result = await sut.Handle(new GetCustomerQuery { Email = mockedCustomer.Email }, CancellationToken.None);

            Assert.IsType<CustomerDto>(result);   
            
            Assert.Equal(mockedCustomer.Name, result.Name);
            Assert.Equal(mockedCustomer.Email, result.Email);
            Assert.Equal(mockedCustomer.MonthlyIncome, result.MonthlyIncome);
            Assert.Equal(mockedCustomer.MonthlyExpense, result.MonthlyExpense);

            mock.Verify(rep => rep.GetCustomerByEmail(mockedCustomer.Email), Times.Once);
        }

        [Fact]
        public async Task GetCustomerDetailNotFound()
        {
            var mockedCustomer = new Customer("test1", "test@test.com.au", 10000, 1000);

            var mock = new Mock<ICustomerRepository>();
            mock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>()));

            var sut = new GetCustomerQueryHandler(_logger, _mapper, mock.Object);

            var exceptionResult = await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new GetCustomerQuery { Email = "test@test.com.au" }, CancellationToken.None));

            Assert.Contains("not found", exceptionResult.Message);
            mock.Verify(rep => rep.GetCustomerByEmail(mockedCustomer.Email), Times.Once);
        }
    }
}
