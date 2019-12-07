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
                Customer customerDetail = await _customerRepository.GetCustomerByEmail(request.Email);

                if (customerDetail != null)
                {
                    if (_customerRepository.IsCustomerEligibleForAccount(customerDetail))
                    {
                        var accountInfo = await _accountRepository.GetAccountByEmail(customerDetail.Email);

                        if (accountInfo == null)
                        {
                            Account newAccount = new Account(request.Email);
                            newAccount.AccountNo = new Random(10000000).Next(10000000, 99999999);
                            newAccount.Active = true;


                            await _accountRepository.AddAccountAsync(newAccount);

                            return _accountMap.Map<AccountDto>(newAccount);
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
