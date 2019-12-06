using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.Handlers.Command
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IMapper customerMap, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerMap = customerMap;
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _customerRepository.EmailExistAsync(request.Email))
            {
                throw new BadRequestException($"Customer with {request.Email} already exists");
            }

            var customer = new Customer(request.Name, request.Email, request.MonthlyIncome, request.MonthlyExpense);

            _customerRepository.Add(customer);

            if (await _customerRepository.SaveChangesAsync() == 0)
            {
                throw new ApplicationException("Unable to save data");
            }

            var customerDto = _customerMap.Map<CustomerDto>(customer);

            return customerDto;

        }
    }
}
