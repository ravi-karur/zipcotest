using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;

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
