using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Common.Exceptions;
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
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto> 
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public GetCustomerQueryHandler(ILogger<GetCustomerQueryHandler> logger,IMapper customerMap,ICustomerDbContext customerDbContext)
        {
            _logger = logger;
            _customerMap = customerMap;
            _customerDbContext = customerDbContext;
        }
        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerInfo = await _customerDbContext.Customers.FindAsync(request.CustomerId);

            if (customerInfo != null)
            {
                _logger.LogInformation($"Got a request get customer Id: {request.CustomerId}");
                return _customerMap.Map<CustomerDto>(customerInfo);
            }

            //return null;  
            throw new NotFoundException($"Customer with {request.CustomerId} was not found");
            
        }
    }
}
