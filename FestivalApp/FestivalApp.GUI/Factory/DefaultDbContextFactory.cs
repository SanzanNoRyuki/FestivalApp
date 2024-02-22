using FestivalApp.DAL;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.GUI.Factory
{
    public class DefaultDbContextFactory : INamedDbContextFactory<FestivalDbContext>
    {
        private readonly string _databaseName;

        public DefaultDbContextFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        public FestivalDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            contextOptionsBuilder.UseSqlServer(_databaseName);
            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}
