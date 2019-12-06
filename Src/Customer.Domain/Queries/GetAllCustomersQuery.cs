using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
