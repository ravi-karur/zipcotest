using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Data.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id").HasDefaultValueSql("newId()");

            builder.Property(b => b.Name).HasColumnName("name").HasMaxLength(50)
                .IsRequired();

            builder.Property(b => b.Email).HasColumnName("email").HasMaxLength(50).IsRequired();

            builder.Property(e => e.MonthlyIncome).HasColumnName("MonthlyIncome").IsRequired();

            builder.Property(e => e.MonthlyExpense).HasColumnName("MonthlyExpense").IsRequired();
            
        }
    }
}
