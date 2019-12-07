using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.Handlers.Query
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public GetAllAccountsQueryHandler(ILogger<GetAllAccountsQueryHandler> logger,IMapper customerMap, IAccountRepository accountRepository)
        {
            _logger = logger;
            _customerMap = customerMap;
            _accountRepository = accountRepository;
        }

        public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            // assuming user is authenticated
            var accounts = await _accountRepository.GetAllAccounts();

            return accounts.Select(x => _customerMap.Map<AccountDto>(x)).ToList();
        }
    }
}
