using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL
{
    public class FestivalDbContext : DbContext
    {
        public FestivalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Festival> Festivals { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Stage> Stages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Show> Shows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Festival>()
                .HasKey(sc => sc.Id);

            modelBuilder
                .Entity<User>()
                .HasKey(sc => sc.Id);

            modelBuilder
                .Entity<Ticket>()
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<Stage>()
                .HasKey(s => s.Id);

            modelBuilder
                .Entity<Artist>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Festival>()
                .HasMany(f => f.Tickets)
                .WithOne(t => t.Festival)
                .HasForeignKey(t => t.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Festival>()
                .HasMany(f => f.Stages)
                .WithOne(s => s.Festival)
                .HasForeignKey(s => s.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Show>()
                .HasKey(show => show.Id);

            modelBuilder.Entity<Stage>()
                .HasMany(s => s.Shows)
                .WithOne(show => show.Stage)
                .HasForeignKey(show => show.StageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Shows)
                .WithOne(show => show.Artist)
                .HasForeignKey(show => show.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne<User>(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne<Address>(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Festivals)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            AddressSeeds.Seed(modelBuilder);
            ArtistSeeds.Seed(modelBuilder);
            FestivalSeeds.Seed(modelBuilder);
            ShowSeeds.Seed(modelBuilder);
            StageSeeds.Seed(modelBuilder);
            TicketSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}