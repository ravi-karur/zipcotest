using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Data.Repositories;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Service.Handlers.Command;
using CustomerApi.Service.UnitTests.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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
            _customerDbContext = fixture.Context;
            _mapper = fixture.Mapper;
            _logger = Mock.Of<ILogger<CreateCustomerCommandHandler>>();
            _customerRepository = new CustomerRepository(fixture.Context);

        }

        [Fact]
        public async void Handle_GivenValidCustomer_ShouldCreateCustomer()
        {
            // Arrange
            
            var sut = new CreateCustomerCommandHandler(_logger, _mapper, _customerRepository);

            var newCustomerCommandRequest = new CreateCustomerCommand { Email = "newcustomer@new.com.au", MonthlyIncome = 20000, MonthlyExpense = 200 };

            // Act
            var result = await sut.Handle(newCustomerCommandRequest, CancellationToken.None);

            var customerFromDb = _customerDbContext.Customers.Find(result.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Guid>(result.Id);
            Assert.Equal(newCustomerCommandRequest.Email, result.Email);
            Assert.Equal(newCustomerCommandRequest.MonthlyExpense, result.MonthlyExpense);
            Assert.Equal(newCustomerCommandRequest.MonthlyIncome, result.MonthlyIncome);

            Assert.NotNull(customerFromDb);
            Assert.Equal(result.Id, customerFromDb.Id);
            Assert.Equal(newCustomerCommandRequest.Email, customerFromDb.Email);
            Assert.Equal(newCustomerCommandRequest.MonthlyExpense, customerFromDb.MonthlyExpense);
            Assert.Equal(newCustomerCommandRequest.MonthlyIncome, customerFromDb.MonthlyIncome);

        }

        [Fact]
        public async void Handle_GivenExistingCustomerEmail_ShouldThrowException()
        {
            // Arrange

            var sut = new CreateCustomerCommandHandler(_logger, _mapper, _customerRepository);

            // Create new customer
            var newCustomerCommandRequest = new CreateCustomerCommand { Email = "newcustomer1@new.com.au", MonthlyIncome = 20000, MonthlyExpense = 200 };
            var successResult = await sut.Handle(newCustomerCommandRequest, CancellationToken.None);

            // Act
            // try to create the same customer

            var exceptionResult = await Assert.ThrowsAsync<BadRequestException>(async () => await sut.Handle(newCustomerCommandRequest, CancellationToken.None) );

            Assert.Contains("exists",exceptionResult.Message);
        }
    }
}
