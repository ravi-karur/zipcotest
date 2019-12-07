using AutoMapper;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;

namespace CustomerApi.Service.Mappers
{
    public class CustomerMapProfile : IMapFrom<CustomerDto> 
    {

        private readonly IMapper _mapper;

        public CustomerMapProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, Domain.Dtos.CustomerDto>()
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dst => dst.MonthlyIncome, opt => opt.MapFrom(src => src.MonthlyIncome))
                    .ForMember(dst => dst.MonthlyExpense, opt => opt.MapFrom(src => src.MonthlyExpense))
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
            });

            _mapper = config.CreateMapper();
        }


        public CustomerDto MapCustomerDto(Domain.Models.Customer customer)
        {
            return _mapper.Map<Domain.Models.Customer, Domain.Dtos.CustomerDto>(customer);
        }

        public void Mapping(Profile profile) => profile.CreateMap<Domain.Models.Customer, CustomerDto>();
    }
}
