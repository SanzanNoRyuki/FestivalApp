using System;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class UserSeeds
    {
        public static readonly User JohnDoe = new User
        {
            Id = Guid.Parse("4e70d683-fa6c-4384-8564-30bb2ec4af3a"),
            Email = "e@mail.com",
            Name = "John Doe",
            AddressId = AddressSeeds.Bozetechova.Id,
            PhoneNumber = "+42 987 654 321",
        };

        public static readonly User MilosZeman = new User
        {
            Id = Guid.Parse("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110"),
            Email = "milos@zeman.cz",
            Name = "Milos Zeman",
            AddressId = AddressSeeds.PragueCastle.Id,
            PhoneNumber = "+42 000 000 000",
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(JohnDoe);
            modelBuilder.Entity<User>().HasData(MilosZeman);
        }
    }
}
