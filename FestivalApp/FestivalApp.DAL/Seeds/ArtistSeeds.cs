using System;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class ArtistSeeds
    {
        public static readonly Artist FreddyMercury = new Artist
        {
            Id = Guid.Parse("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"),
            Name = "Freddy Mercury",
            Country = "Great Britain",
            Genre = "Rock",
            ShortDescription = "Mercury defied the conventions of a rock frontman, " +
                               "with his highly theatrical style influencing the" +
                                "artistic direction of Queen.",
            LongDescription = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                              "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
            PhotoPath = "pic/freddy.jpg"
        };

        public static readonly Artist Queen = new Artist
        {
            Id = Guid.Parse("5469990b-d1f9-49a6-ae50-800270d77b88"),
            Name = "Queen",
            Country = "Great Britain",
            Genre = "Rock",
            ShortDescription = "Short description...",
            LongDescription = "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" +
                              "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
            PhotoPath = "pic/Queen.png"
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().HasData(FreddyMercury);
            modelBuilder.Entity<Artist>().HasData(Queen);
        }
    }
}