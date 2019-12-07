using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Models;
using CustomerApi.Service.Handlers.Command;
using CustomerApi.Service.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace CustomerApi.Service.UnitTests.Commands
{
    [Collection("CommandCollection")]
    public class CreateCustomerCommandTest : CommandTestBase
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandTest(CommandTestFixture fixture)
        {
            _customerDbContext = null; //fixture.Context;
            _mapper = fixture.Mapper;
            _logger = Mock.Of<ILogger<CreateCustomerCommandHandler>>();
            

        }

        [Fact]
        public async void Handle_GivenValidCustomer_ShouldCreateCustomer()
        {
            // Arrange
            var mockedCustomer1 = new Customer("test1", "test1@test.com.au", 10000, 1000);

            var customerList = new List<Customer>() { mockedCustomer1 };
            var mock = new Mock<ICustomerRepository>();

            //Mock to return empty customer
            mock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>()));
            mock.Setup(srv => srv.AddCustomerAsync(It.IsAny<Customer>()));

            var sut = new CreateCustomerCommandHandler(_logger, _mapper, mock.Object);

            var newCustomerCommandRequest = new CreateCustomerCommand { Email = mockedCustomer1.Email, MonthlyIncome = mockedCustomer1.MonthlyIncome, MonthlyExpense = mockedCustomer1.MonthlyExpense };

            // Act
            var result = await sut.Handle(newCustomerCommandRequest, CancellationToken.None);


            // Assert
            Assert.NotNull(result);            
            Assert.Equal(newCustomerCommandRequest.Email, result.Email);
            Assert.Equal(newCustomerCommandRequest.MonthlyExpense, result.MonthlyExpense);
            Assert.Equal(newCustomerCommandRequest.MonthlyIncome, result.MonthlyIncome);

            mock.Verify(rep => rep.GetCustomerByEmail(mockedCustomer1.Email), Times.Once);
            mock.Verify(rep => rep.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_GivenExistingCustomerEmail_ShouldThrowException()
        {
            // Arrange
            var mockedCustomer1 = new Customer("test1", "test1@test.com.au", 10000, 1000);

            var customerList = new List<Customer>() { mockedCustomer1 };
            var mock = new Mock<ICustomerRepository>();

            //Mock to return empty customer
            mock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(mockedCustomer1);
            mock.Setup(srv => srv.AddCustomerAsync(It.IsAny<Customer>()));

            var sut = new CreateCustomerCommandHandler(_logger, _mapper, mock.Object);

            // Create new customer
            var newCustomerCommandRequest = new CreateCustomerCommand { Email = "newcustomer1@new.com.au", MonthlyIncome = 20000, MonthlyExpense = 200 };
            
            // Act
            // try to create the same customer

            var exceptionResult = await Assert.ThrowsAsync<BadRequestException>(async () => await sut.Handle(newCustomerCommandRequest, CancellationToken.None) );

            Assert.Contains("exists",exceptionResult.Message);
        }
    }
}
