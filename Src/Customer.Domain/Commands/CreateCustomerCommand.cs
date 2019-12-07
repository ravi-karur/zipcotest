using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Domain.Commands
{
    public class CreateCustomerCommand : CommandBase<CustomerDto>
    {
        [JsonConstructor]
        public CreateCustomerCommand()
        {
        }

        
        
        public CreateCustomerCommand(string name, string email, uint monthlyIncome, uint monthlyExpense)
        {
            Name = name;
            Email = email;
            MonthlyIncome = monthlyIncome;
            MonthlyExpense = monthlyExpense;
        }


        [JsonProperty("name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        [JsonProperty("email")]
        [Required]
        [MaxLength(50)]
        [JsonRequired]
        [EmailAddress]
        public string Email { get; set; }

        [JsonProperty("monthlyIncome")]
        [Range(0,int.MaxValue,ErrorMessage ="Monthly Income must be positive" )]
        public uint MonthlyIncome { get; set; }

        [JsonProperty("monthlyExpense")]
        [Range(0, int.MaxValue, ErrorMessage = "Monthly expense must be positive")]
        public uint MonthlyExpense { get; set; }
    }
}
