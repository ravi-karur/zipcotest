using CustomerApi.Domain.Dtos;

namespace CustomerApi.Service.Mappers
{
    public interface ICustomerMapProfile
    {
        public CustomerDto MapCustomerDto(Domain.Models.Customer customer);
    }
}
