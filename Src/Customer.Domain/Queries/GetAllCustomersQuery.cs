using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomerApi.Domain.Queries
{
    public class GetAllCustomersQuery : QueryBase<List<CustomerDto>>
    {
        
        [JsonConstructor]
        public GetAllCustomersQuery()
        {
        }

    }
}
