using AutoMapper;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Service.Mappers
{
    public class AccountMapProfile : IMapFrom<AccountDto> 
    {

        private readonly IMapper _mapper;

        public AccountMapProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, Domain.Dtos.AccountDto>()
                    .ForMember(dst => dst.AccountNo, opt => opt.MapFrom(src => src.AccountNo))
                    .ForMember(dst => dst.customerId, opt => opt.MapFrom(src => src.CustomerId))
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
            });

            _mapper = config.CreateMapper();
        }


        public AccountDto MapCustomerDto(Domain.Models.Account account)
        {
            return _mapper.Map<Domain.Models.Account, Domain.Dtos.AccountDto>(account);
        }

        public void Mapping(Profile profile) => profile.CreateMap<Domain.Models.Account, AccountDto>()
                    .ForMember(dst => dst.AccountNo, opt => opt.MapFrom(src => src.AccountNo))
                    .ForMember(dst => dst.customerId, opt => opt.MapFrom(src => src.CustomerId))
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
    }
}
