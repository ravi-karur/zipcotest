using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }

        public Customer GetCustomerByEmail(string email)
        {
            var customerDetail = ModelDbSets.AsNoTracking().Where(e => e.Email.Equals(email)).FirstOrDefault();

            if ( customerDetail != null)
            {
                return customerDetail;
            }

            throw new NotFoundException($"Customer with email {email} was not found");
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await ModelDbSets.AsNoTracking().AnyAsync(e => e.Email.Equals(email));
        }


    }
}
