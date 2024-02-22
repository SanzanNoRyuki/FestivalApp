using System;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class TicketSeeds
    {
        public static readonly Ticket RockForPeopleOne = new Ticket
        {
            Id = Guid.Parse("4e70d683-fa6c-4384-8564-30bb2ec4af3a"),
            Type = TicketType.Basic,
            Length = TicketLength.FestivalWide,
            PriceAmount = Convert.ToDecimal(1500),
            PriceCurrency = Currency.Czk,
            StartDate = new DateTime(2021, 06, 10, 15, 00, 00),
            UserId = UserSeeds.MilosZeman.Id,
            FestivalId = FestivalSeeds.RFP.Id
        };

        public static readonly Ticket RockForPeopleTwo = new Ticket
        {
            Id = Guid.Parse("ff603e7c-9ee3-4e54-8d3f-6ef2796e9113"),
            Type = TicketType.Vip,
            Length = TicketLength.FestivalWide,
            PriceAmount = Convert.ToDecimal(3000),
            PriceCurrency = Currency.Czk,
            StartDate = new DateTime(2021, 06, 10, 15, 00, 00),
            UserId = UserSeeds.JohnDoe.Id,
            FestivalId = FestivalSeeds.RFP.Id
        };

        public static readonly Ticket Pohoda = new Ticket
        {
            Id = Guid.Parse("ef603e7c-9ee3-4e54-8d3f-6ef2796e9111"),
            Type = TicketType.Basic,
            Length = TicketLength.OneDay,
            PriceAmount = Convert.ToDecimal(30),
            PriceCurrency = Currency.Euro,
            StartDate = new DateTime(2021, 07, 05, 12, 00, 00),
            UserId = UserSeeds.MilosZeman.Id,
            FestivalId = FestivalSeeds.Pohoda.Id
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasData(RockForPeopleOne);
            modelBuilder.Entity<Ticket>().HasData(RockForPeopleTwo);
            modelBuilder.Entity<Ticket>().HasData(Pohoda);
        }
    }
}