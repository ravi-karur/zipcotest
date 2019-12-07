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
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public GetAllCustomersQueryHandler(ILogger<GetAllCustomersQueryHandler> logger,IMapper customerMap, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerMap = customerMap;
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            // assuming user is authenticated
            var customers = await _customerRepository.GetAllCustomers();

            return customers.Select(x => _customerMap.Map<CustomerDto>(x)).ToList();

        }
    }
}
