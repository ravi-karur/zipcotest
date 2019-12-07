namespace CustomerApi.Service.UnitTests.Common
{
    public class CustomerContextFactory
    {
        public static Data.Persistence.DbContext Create()
        {
            //var options = new DbContextOptionsBuilder<Data.Persistence.DbContext>()
            //    .UseInMemoryDatabase(Guid.NewGuid().ToString())
            //    .Options;

            //var context = new Data.Persistence.DbContext(options);

            //context.Database.EnsureCreated();

            //context.Customers.AddRange(new[] {
            //    new Customer("Test1","Test1@test.com.au",10000,1000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B05FB") },
            //    new Customer("Test2","Test2@test.com.au",10000,2000) { Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B06FB") },
            //    new Customer("Test3","Test3@test.com.au",10000,10000){ Id = Guid.Parse("ACA5A74B-CD2C-441B-9795-632FBC0B07FB") },
            //});

            //context.SaveChanges();

            //return context;

            return null;
        }

        public static void Destroy(Data.Persistence.DbContext context)
        {
            //context.Database.EnsureDeleted();

            //context.Dispose();
        }
    }
}
