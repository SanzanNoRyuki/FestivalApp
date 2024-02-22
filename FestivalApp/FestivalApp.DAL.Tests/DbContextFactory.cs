using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Tests
{
    public class DbContextFactory
    {
        private readonly string _dbName;

        public DbContextFactory(string dbName)
        {
            _dbName = dbName;
        }
        public FestivalDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(_dbName);

            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}