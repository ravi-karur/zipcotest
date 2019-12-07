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
            _customerDbContext = null;
            _mapper = fixture.Mapper;
            _customerLogger = Mock.Of<ILogger<CreateCustomerCommandHandler>>();
            _accountLogger = Mock.Of<ILogger<CreateAccountCommandHandler>>();
            _customerRepository = null; //new CustomerRepository(fixture.Context);
            _accountRepository = null; //AccountRepository(fixture.Context);

        }

        [Fact]
        public async void Handle_GivenValidCustomer_ShouldCreateAccount()
        {
            // Arrange
            var mockedCustomer1 = new Customer("test1", "test1@test.com.au", 10000, 1000);
            var mockedAccountRequest = new Account("test1@test.com.au");


            var customerList = new List<Customer>() { mockedCustomer1 };
            var customerMock = new Mock<ICustomerRepository>();
            //Mock to return empty customer
            customerMock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(mockedCustomer1);
            customerMock.Setup(srv => srv.IsCustomerEligibleForAccount(It.IsAny<Customer>())).Returns(true);


            var accountMock = new Mock<IAccountRepository>();
            accountMock.Setup(srv => srv.AddAccountAsync(It.IsAny<Account>()));

            var newAccountCommandRequest = new CreateAccountCommand { Email = mockedCustomer1.Email };

            // Act
            var accountCreationSUT = new CreateAccountCommandHandler(_accountLogger, _mapper,accountMock.Object, customerMock.Object);

            var accountCreationResult = await accountCreationSUT.Handle(newAccountCommandRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(accountCreationResult);
            Assert.True(accountCreationResult.AccountNo > 0);
            Assert.Equal(accountCreationResult.Email, mockedCustomer1.Email);

            customerMock.Verify(rep => rep.GetCustomerByEmail(mockedCustomer1.Email), Times.Once);
            customerMock.Verify(rep => rep.IsCustomerEligibleForAccount(mockedCustomer1), Times.Once);
            accountMock.Verify(rep => rep.AddAccountAsync(It.IsAny<Account>()), Times.Once);

        }

        [Fact]
        public async void Handle_GivenCustomerNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var mockedCustomer1 = new Customer("test1", "test1@test.com.au", 10000, 1000);
            var mockedAccountRequest = new Account("test2@test.com.au");


            var customerList = new List<Customer>() { mockedCustomer1 };
            var customerMock = new Mock<ICustomerRepository>();
            //Mock to return empty customer
            customerMock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>()));
            customerMock.Setup(srv => srv.IsCustomerEligibleForAccount(It.IsAny<Customer>())).Returns(true);


            var accountMock = new Mock<IAccountRepository>();
            accountMock.Setup(srv => srv.AddAccountAsync(It.IsAny<Account>()));

            var newAccountCommandRequest = new CreateAccountCommand { Email = mockedCustomer1.Email };

            // Act
            var accountCreationSUT = new CreateAccountCommandHandler(_accountLogger, _mapper, accountMock.Object, customerMock.Object);

            var accountCreationExceptionResult = await Assert.ThrowsAsync<NotFoundException>(async () => await accountCreationSUT.Handle(newAccountCommandRequest, CancellationToken.None));


            // Assert
            Assert.NotNull(accountCreationExceptionResult);            
            customerMock.Verify(rep => rep.GetCustomerByEmail(mockedCustomer1.Email), Times.Once);
            customerMock.Verify(rep => rep.IsCustomerEligibleForAccount(mockedCustomer1), Times.Never);
            accountMock.Verify(rep => rep.AddAccountAsync(It.IsAny<Account>()), Times.Never);

        }

        [Fact]
        public async void Handle_GivenCustomerWithInSufficientBalance_ShouldThrowBadRequest()
        {
            // Arrange
            var mockedCustomer1 = new Customer("test1", "test1@test.com.au", 10000, 9500);
            var mockedAccountRequest = new Account("test1@test.com.au");


            var customerList = new List<Customer>() { mockedCustomer1 };
            var customerMock = new Mock<ICustomerRepository>();
            //Mock to return empty customer
            customerMock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(mockedCustomer1);
            customerMock.Setup(srv => srv.IsCustomerEligibleForAccount(It.IsAny<Customer>())).Returns(false);


            var accountMock = new Mock<IAccountRepository>();
            accountMock.Setup(srv => srv.AddAccountAsync(It.IsAny<Account>()));

            var newAccountCommandRequest = new CreateAccountCommand { Email = mockedCustomer1.Email };

            // Act
            var accountCreationSUT = new CreateAccountCommandHandler(_accountLogger, _mapper, accountMock.Object, customerMock.Object);

            var accountCreationExceptionResult = await Assert.ThrowsAsync<BadRequestException>(async () => await accountCreationSUT.Handle(newAccountCommandRequest, CancellationToken.None));
            

            // Assert
            Assert.NotNull(accountCreationExceptionResult);
            customerMock.Verify(rep => rep.GetCustomerByEmail(mockedCustomer1.Email), Times.Once);
            customerMock.Verify(rep => rep.IsCustomerEligibleForAccount(mockedCustomer1), Times.Once);
            accountMock.Verify(rep => rep.AddAccountAsync(It.IsAny<Account>()), Times.Never); ;


        }

    }
}
