using CustomerApi.API.Tests.Common;
using CustomerService;
using CustomerService.Controllers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

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

            var result = await controller.GetCustomerAsync("unknown@test.com.au");
            
            Assert.NotNull(result);
        }
    }
}
