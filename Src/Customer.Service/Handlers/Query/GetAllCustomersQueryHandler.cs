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
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public GetAllCustomersQueryHandler(ILogger<GetAllCustomersQueryHandler> logger,IMapper customerMap,ICustomerDbContext customerDbContext)
        {
            _logger = logger;
            _customerMap = customerMap;
            _customerDbContext = customerDbContext;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            // assuming user is authenticated
            var customers = await _customerDbContext.Customers.ProjectTo<CustomerDto>(_customerMap.ConfigurationProvider).ToListAsync();

            return customers;

        }
    }
}
