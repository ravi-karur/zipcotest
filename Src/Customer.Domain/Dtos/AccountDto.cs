using Newtonsoft.Json;
using System;

namespace CustomerApi.Domain.Dtos
{
    public class AccountDto
    {
        [JsonProperty("accountNo")]
        public long AccountNo { get; set; }

        [JsonProperty("customerId")]
        public Guid customerId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

    }
}
