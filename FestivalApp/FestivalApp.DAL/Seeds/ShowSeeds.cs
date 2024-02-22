using System;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class ShowSeeds
    {
        public static readonly Show FreddyRFP = new Show
        {
            Id = Guid.Parse("8837e270-2243-4f59-98ba-b2189dcae3ce"),
            ArtistId = ArtistSeeds.FreddyMercury.Id,
            StageId = StageSeeds.StageWestRFP.Id,
            StartPlaying = new DateTime(2021, 06, 10, 17, 00, 00),
            EndPlaying = new DateTime(2021, 06, 10, 18, 40, 00),
            LengthOfPlaying = new TimeSpan(01, 40, 00)
        };

        public static readonly Show QueenRFP = new Show
        {
            Id = Guid.Parse("3c0cb3fa-2be5-408a-af45-f258a5d3df00"),
            ArtistId = ArtistSeeds.Queen.Id,
            StageId = StageSeeds.StageEastRFP.Id,
            StartPlaying = new DateTime(2021, 06, 10, 20, 00, 00),
            EndPlaying = new DateTime(2021, 06, 10, 22, 00, 00),
            LengthOfPlaying = new TimeSpan(02, 00, 00)
        };

        public static readonly Show FreddyPohoda = new Show
        {
            Id = Guid.Parse("7714e9e2-8bba-49d3-b72f-184d94afa736"),
            ArtistId = ArtistSeeds.FreddyMercury.Id,
            StageId = StageSeeds.StageMainPohoda.Id,
            StartPlaying = new DateTime(2021, 07, 04, 18, 00, 00),
            EndPlaying = new DateTime(2021, 07, 04, 19, 30, 00),
            LengthOfPlaying = new TimeSpan(01, 30, 00)
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>().HasData(FreddyRFP);
            modelBuilder.Entity<Show>().HasData(QueenRFP);
            modelBuilder.Entity<Show>().HasData(FreddyPohoda);
        }
    }
}