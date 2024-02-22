using System;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class FestivalSeeds
    {
        public static readonly Festival RFP = new Festival
        {
            Id = Guid.Parse("263aed9b-0cfc-4621-beef-8bfa8a001b81"),
            Title = "Rock For People",
            Start = new DateTime(2021, 06, 10, 15, 00, 00),
            End = new DateTime(2021, 06, 13, 12, 00, 00),
            Capacity = 10000,
            UserId = UserSeeds.JohnDoe.Id
        };

        public static readonly Festival Pohoda = new Festival
        {
            Id = Guid.Parse("424eebbe-f047-48f5-9db3-051f753d5c13"),
            Title = "Pohoda",
            Start = new DateTime(2021, 07, 04, 12, 00, 00),
            End = new DateTime(2021, 07, 7, 16, 00, 00),
            Capacity = 10000,
            UserId = UserSeeds.JohnDoe.Id
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Festival>().HasData(RFP);
            modelBuilder.Entity<Festival>().HasData(Pohoda);
        }
    }
}