using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.Models
{
    public class Customer : ModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public uint MonthlyIncome { get; set; }
        public uint MonthlyExpense { get; set; }
        
        public Customer(string name, string email, uint monthlyIncome, uint monthlyExpense)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            MonthlyIncome = monthlyIncome;
            MonthlyExpense = monthlyExpense;
        }
    }
}
