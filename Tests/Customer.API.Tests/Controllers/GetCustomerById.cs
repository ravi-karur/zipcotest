using CustomerApi.API.Tests.Common;
using CustomerApi.Domain.Dtos;
using CustomerService;
using CustomerService.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CustomerApi.Data.Persistence;

namespace CustomerApi.API.Tests.Controllers
{
    public class GetCustomerById : IClassFixture<CustomWebHostBuilderFactory<Startup>>
    {
        private readonly CustomWebHostBuilderFactory<Startup> _factory;
        private Mock<IMediator> _mediator = new Mock<IMediator>();
        private Mock<ILogger<CustomerController>> 
            _logger= new Mock<ILogger<CustomerController>>();

        public GetCustomerById(CustomWebHostBuilderFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenCustomerId_ReturnsCustomerModel()
        {

            var controller = new CustomerController(_logger.Object, _mediator.Object);

            var customerIdtoBeSearched = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B05FB");

            var result = await controller.GetCustomerAsync(customerIdtoBeSearched);

            Assert.Null(result);
        }
    }
}
