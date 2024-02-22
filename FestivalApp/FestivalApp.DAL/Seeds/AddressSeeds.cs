using System;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class AddressSeeds
    {
        public static readonly Address Bozetechova = new Address
        {
            Id = Guid.Parse("d6a8bbe4-ecbb-43c9-b9ea-e6ce7a734f4d"),
            AddressLine1 = "Bozetechova",
            AddressLine2 = "2/1",
            City = "Brno",
            State = "Jihomoravsky",
            Country = "Czechia",
            PostalCode = "612 00",
            UserId = Guid.Parse("4e70d683-fa6c-4384-8564-30bb2ec4af3a") // John Doe
        };

        public static readonly Address PragueCastle = new Address
        {
            Id = Guid.Parse("66c446a1-2447-4f58-aecd-e385c02217b9"),
            AddressLine1 = "Hradčany",
            AddressLine2 = null,
            City = "Prague",
            State = "Prazsky",
            Country = "Czechia",
            PostalCode = "119 08",
            UserId = Guid.Parse("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110") // Milos Zeman
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(Bozetechova);
            modelBuilder.Entity<Address>().HasData(PragueCastle);
        }
    }
}
