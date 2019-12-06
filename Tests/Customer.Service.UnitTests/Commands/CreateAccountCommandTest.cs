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
    public class CreateAccountCommandTest : CommandTestBase
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerCommandHandler> _customerLogger;
        private readonly ILogger<CreateAccountCommandHandler> _accountLogger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandTest(CommandTestFixture fixture)
        {
            _customerDbContext = fixture.Context;
            _mapper = fixture.Mapper;
            _customerLogger = Mock.Of<ILogger<CreateCustomerCommandHandler>>();
            _accountLogger = Mock.Of<ILogger<CreateAccountCommandHandler>>();
            _customerRepository = new CustomerRepository(fixture.Context);
            _accountRepository = new AccountRepository(fixture.Context);

        }

        [Fact]
        public async void Handle_GivenValidCustomer_ShouldCreateAccount()
        {
            // Arrange
            
            // Create Customer
            var customerCreation = new CreateCustomerCommandHandler(_customerLogger, _mapper, _customerRepository);
            var newCustomerCommandRequest = new CreateCustomerCommand { Email = "newcustomerForAccount@new.com.au", MonthlyIncome = 20000, MonthlyExpense = 200 };
            var customerCreationResult = await customerCreation.Handle(newCustomerCommandRequest, CancellationToken.None);

            var newAccountCommandRequest = new CreateAccountCommand { Email = customerCreationResult.Email, customerId = customerCreationResult.Id };

            // Act
            var accountCreationSUT = new CreateAccountCommandHandler(_accountLogger, _mapper,_accountRepository, _customerRepository);

            var accountCreationResult = await accountCreationSUT.Handle(newAccountCommandRequest, CancellationToken.None);

            var customerFromDb = _customerDbContext.Customers.Find(customerCreationResult.Id);
            var accountFromDb = _customerDbContext.Accounts.Find(accountCreationResult.AccountNo);

            // Assert
            Assert.NotNull(customerCreationResult);
            Assert.IsType<Guid>(customerCreationResult.Id);
            Assert.Equal(newCustomerCommandRequest.Email, customerCreationResult.Email);
            Assert.Equal(newCustomerCommandRequest.MonthlyExpense, customerCreationResult.MonthlyExpense);
            Assert.Equal(newCustomerCommandRequest.MonthlyIncome, customerCreationResult.MonthlyIncome);

            Assert.NotNull(customerFromDb);
            Assert.Equal(customerCreationResult.Id, customerFromDb.Id);
            Assert.Equal(newCustomerCommandRequest.Email, customerFromDb.Email);
            Assert.Equal(newCustomerCommandRequest.MonthlyExpense, customerFromDb.MonthlyExpense);
            Assert.Equal(newCustomerCommandRequest.MonthlyIncome, customerFromDb.MonthlyIncome);


            Assert.NotNull(accountCreationResult);
            Assert.Equal(customerCreationResult.Id,accountCreationResult.customerId);
            Assert.Equal(customerCreationResult.Email, accountCreationResult.Email);
            Assert.True(accountFromDb.Active);

            Assert.NotNull(accountFromDb);
            Assert.Equal(accountCreationResult.customerId, accountFromDb.CustomerId);
            Assert.Equal(accountCreationResult.Email, accountFromDb.Email);
            

        }

        [Fact]
        public async void Handle_GivenCustomerNotExist_ShouldThrowNotFoundException()
        {
            // Arrange

            // Create Customer           

            var notCustomerEmail = "notacustomer@test.com.au";

            var newAccountCommandRequest = new CreateAccountCommand { Email = notCustomerEmail, customerId = Guid.NewGuid() };

            // Act
            var accountCreationSUT = new CreateAccountCommandHandler(_accountLogger, _mapper, _accountRepository, _customerRepository);

            var accountCreationExceptionResult = await Assert.ThrowsAsync<NotFoundException>(async () => await accountCreationSUT.Handle(newAccountCommandRequest, CancellationToken.None));

            var accountFromDb = _accountRepository.GetAccountByEmail(notCustomerEmail);
            
            // Assert
            Assert.Null(accountFromDb);
            

        }

        [Fact]
        public async void Handle_GivenCustomerWithInSufficientBalance_ShouldThrowBadRequest()
        {
            // Arrange

            // Create Customer
            var customerCreation = new CreateCustomerCommandHandler(_customerLogger, _mapper, _customerRepository);
            var newCustomerCommandRequest = new CreateCustomerCommand { Email = "customerwithlessthan1000@test.com.au", MonthlyIncome = 2000, MonthlyExpense = 1200 };
            var customerCreationResult = await customerCreation.Handle(newCustomerCommandRequest, CancellationToken.None);

            var newAccountCommandRequest = new CreateAccountCommand { Email = customerCreationResult.Email, customerId = customerCreationResult.Id };

            // Act
            var accountCreationSUT = new CreateAccountCommandHandler(_accountLogger, _mapper, _accountRepository, _customerRepository);

            var accountCreationExceptionResult = await Assert.ThrowsAsync<BadRequestException>(async () => await accountCreationSUT.Handle(newAccountCommandRequest, CancellationToken.None));

            var accountFromDb = _accountRepository.GetAccountByEmail(customerCreationResult.Email);

            // Assert
            Assert.Null(accountFromDb);


        }

    }
}
