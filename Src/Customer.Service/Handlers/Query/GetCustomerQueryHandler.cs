using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.Handlers.Query
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto> 
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public GetCustomerQueryHandler(ILogger<GetCustomerQueryHandler> logger,IMapper customerMap,ICustomerRepository repository)
        {
            _logger = logger;
            _customerMap = customerMap;
            _repository = repository;
        }
        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerInfo = await _repository.GetCustomerByEmail(request.Email);

            if (customerInfo != null)
            {
                _logger.LogInformation($"Got a request get customer Id: {request.Email}");
                return _customerMap.Map<CustomerDto>(customerInfo);
            }

            //return null;  
            throw new NotFoundException($"Customer with {request.Email} was not found");
            
        }
    }
}
