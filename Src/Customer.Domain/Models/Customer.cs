using MongoDB.Bson.Serialization.Attributes;

namespace CustomerApi.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Customer : ModelBase
    {   
        public string Name { get; set; }
        public string Email { get; set; }
        public uint MonthlyIncome { get; set; }
        public uint MonthlyExpense { get; set; }

        public Customer(string name, string email, uint monthlyIncome, uint monthlyExpense)
        {   
            Name = name;
            Email = email;
            MonthlyIncome = monthlyIncome;
            MonthlyExpense = monthlyExpense;
        }
    }
}
