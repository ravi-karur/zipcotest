using CustomerApi.Data.Persistence;
using Newtonsoft.Json;
using System.Net.Http;
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

        public static void InitializeDbForTests(DbContext context)
        {
            //context.Customers.InsertOneAsync(new Customer("Test1", "Test1@test.com.au", 10000, 1000) ,
            //    new Customer("Test2", "Test2@test.com.au", 10000, 2000),
            //    new Customer("TestWithNoBalance", "Test3@test.com.au", 10000, 10000)) ;

            
            //context.SaveChanges();
        }
    }
}
