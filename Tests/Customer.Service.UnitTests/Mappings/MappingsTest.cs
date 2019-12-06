using AutoMapper;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using Xunit;

namespace CustomerApi.Service.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapCustomerToCustomerDto()
        {
            var entity = new Customer("test", "test@test.com.au", 1000, 500);

            

            var result = _mapper.Map<CustomerDto>(entity);

            Assert.NotNull(result);
            Assert.IsType<CustomerDto>(result);
        }
    }
}
