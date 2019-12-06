using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using CustomerApi.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.Handlers.Query
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public GetAllAccountsQueryHandler(ILogger<GetAllAccountsQueryHandler> logger,IMapper customerMap,ICustomerDbContext customerDbContext)
        {
            _logger = logger;
            _customerMap = customerMap;
            _customerDbContext = customerDbContext;
        }

        public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            // assuming user is authenticated
            var accounts = await _customerDbContext.Accounts.ProjectTo<AccountDto>(_customerMap.ConfigurationProvider).ToListAsync();

            return accounts;
        }
    }
}
