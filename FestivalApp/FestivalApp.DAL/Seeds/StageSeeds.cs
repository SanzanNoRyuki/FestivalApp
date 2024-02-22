using System;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class StageSeeds
    {
        public static readonly Stage StageEastRFP = new Stage
        {
            Id = Guid.Parse("40d3eb9b-8a13-46b4-815e-a0dfb1bef9be"),
            Name = "Stage East",
            Description = "Stage east of gate",
            PhotoPath = "stages/east.png",
            FestivalId = FestivalSeeds.RFP.Id
        };

        public static readonly Stage StageWestRFP = new Stage
        {
            Id = Guid.Parse("fa804f29-e366-4018-b38b-71af32d8142f"),
            Name = "Stage West",
            Description = "Stage west of gate",
            PhotoPath = "stages/west.png",
            FestivalId = FestivalSeeds.RFP.Id
        };

        public static readonly Stage StageMainPohoda = new Stage
        {
            Id = Guid.Parse("75fcfe46-c596-4d93-aa11-89574b2c7574"),
            Name = "Main Stage",
            Description = "Only stage",
            PhotoPath = "stages/main.png",
            FestivalId = FestivalSeeds.Pohoda.Id
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stage>().HasData(StageEastRFP);
            modelBuilder.Entity<Stage>().HasData(StageWestRFP);
            modelBuilder.Entity<Stage>().HasData(StageMainPohoda);
        }
    }
}