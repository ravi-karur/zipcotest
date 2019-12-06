using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace CustomerApi.API.Tests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(CustomerDbContext context)
        {
            context.Customers.AddRange(new Customer("Test1", "Test1@test.com.au", 10000, 1000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B05FB") },
                new Customer("Test2", "Test2@test.com.au", 10000, 2000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B06FB") },
                new Customer("TestWithNoBalance", "Test3@test.com.au", 10000, 10000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B07FB") });

            
            context.SaveChanges();
        }
    }
}
