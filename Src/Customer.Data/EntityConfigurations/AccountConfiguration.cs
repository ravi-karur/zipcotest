using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Data.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.HasKey(b => b.AccountNo);
            builder.Property(b => b.AccountNo).HasColumnName("AccountNo");


            builder.Property(b => b.Email).HasColumnName("email").HasMaxLength(50).IsRequired();

            builder.Property(e => e.CustomerId).HasColumnName("CustomerId").IsRequired();

            builder.Property(e => e.Active).HasColumnName("IsActive").IsRequired();
            
        }
    }
}
