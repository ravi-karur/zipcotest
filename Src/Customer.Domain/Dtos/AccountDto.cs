using Newtonsoft.Json;

namespace CustomerApi.Domain.Dtos
{
    public class AccountDto
    {
        [JsonProperty("accountNo")]
        public long AccountNo { get; set; }


        [JsonProperty("email")]
        public string Email { get; set; }

    }
}
