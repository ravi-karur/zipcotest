using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.Handlers.Command
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _accountMap;
        private readonly ILogger _logger;

        public CreateAccountCommandHandler(ILogger<CreateAccountCommandHandler> logger, IMapper accountMap, IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _accountMap = accountMap;
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Customer customerDetail = _customerRepository.GetCustomerByEmail(request.Email);

                if (customerDetail != null)
                {
                    if (_accountRepository.IsCustomerEligibleForAccount(customerDetail))
                    {
                        var accountInfo = _accountRepository.GetAccountByCustomerId(customerDetail.Id);

                        if (accountInfo is null)
                        {
                            Account newAccount = new Account(request.Email, customerDetail.Id);
                            newAccount.Active = true;


                            _accountRepository.Add(newAccount);
                            if (await _customerRepository.SaveChangesAsync() == 0)
                            {
                                throw new ApplicationException("Unable to save data");
                            }
                            var customerDto = _accountMap.Map<AccountDto>(newAccount);

                            return customerDto;
                        }
                        else
                        {
                            throw new BadRequestException("Customer already has an account");
                        }
                    }
                    else
                    {
                        throw new BadRequestException("Customer not eligible for ZipPay credit");
                    }

                }
                else
                {
                    throw new NotFoundException("Customer does not exist and need to register before creating an account");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }



        }
    }
}
