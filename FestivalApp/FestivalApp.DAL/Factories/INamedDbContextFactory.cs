using Microsoft.EntityFrameworkCore;
namespace FestivalApp.DAL.Factories
{
    public interface INamedDbContextFactory<out TDbContext> where TDbContext : DbContext
    {
        TDbContext Create();
    }
}