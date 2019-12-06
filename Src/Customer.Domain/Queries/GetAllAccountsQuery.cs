using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApi.Domain.Queries
{
    public class GetAllAccountsQuery : QueryBase<List<AccountDto>>
    {
        [JsonConstructor]
        public GetAllAccountsQuery()
        {
           
        }
    }
}
