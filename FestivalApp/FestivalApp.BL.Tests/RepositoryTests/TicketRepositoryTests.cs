using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Models.Enums;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{

    public sealed class TicketRepositoryTests : IDisposable
    {
        // Setup
        private readonly TicketRepository _ticketRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public TicketRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(TicketRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _ticketRepositorySUT = new TicketRepository(_dbContextFactory);
        }

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }

        // Base tests
        [Fact]
        public void GetById_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Tickets.Find(id));

            var dbModel = _ticketRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var entity = TicketSeeds.Pohoda;
            entity.User = UserSeeds.MilosZeman;
            entity.Festival = FestivalSeeds.Pohoda;
            var seedModel = TicketMapper.TicketToTicketDetailModel(entity);

            var dbModel = _ticketRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Type, dbModel.Type);
            Assert.Equal(seedModel.Length, dbModel.Length);
            Assert.Equal(seedModel.PriceAmount, dbModel.PriceAmount);
            Assert.Equal(seedModel.PriceCurrency, dbModel.PriceCurrency);
            Assert.Equal(seedModel.StartDate, dbModel.StartDate);
            Assert.Equal(seedModel.UserId, dbModel.UserId);
            Assert.Equal(seedModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(seedModel.UserName, dbModel.UserName);
            Assert.Equal(seedModel.FestivalId, dbModel.FestivalId);
            Assert.Equal(seedModel.FestivalTitle, dbModel.FestivalTitle);
            Assert.Equal(seedModel.FestivalStart, dbModel.FestivalStart);
            Assert.Equal(seedModel.FestivalEnd, dbModel.FestivalEnd);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new TicketDetailModel
            {
                Type = TicketType.Basic,
                Length = TicketLength.OneDay,
                PriceAmount = Convert.ToDecimal(30),
                PriceCurrency = Currency.Euro,
                StartDate = new DateTime(2021, 07, 05, 12, 00, 00),

                UserId = UserSeeds.JohnDoe.Id,
                UserEmail = UserSeeds.JohnDoe.Email,
                UserName = UserSeeds.JohnDoe.Name,

                FestivalId = FestivalSeeds.Pohoda.Id,
                FestivalTitle = FestivalSeeds.Pohoda.Title,
                FestivalStart = FestivalSeeds.Pohoda.Start,
                FestivalEnd = FestivalSeeds.Pohoda.End,
            };

            var dbModel = _ticketRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.Type, dbModel.Type);
            Assert.Equal(newModel.Length, dbModel.Length);
            Assert.Equal(newModel.PriceAmount, dbModel.PriceAmount);
            Assert.Equal(newModel.PriceCurrency, dbModel.PriceCurrency);
            Assert.Equal(newModel.StartDate, dbModel.StartDate);
            Assert.Equal(newModel.UserId, dbModel.UserId);
            Assert.Equal(newModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(newModel.UserName, dbModel.UserName);
            Assert.Equal(newModel.FestivalId, dbModel.FestivalId);
            Assert.Equal(newModel.FestivalTitle, dbModel.FestivalTitle);
            Assert.Equal(newModel.FestivalStart, dbModel.FestivalStart);
            Assert.Equal(newModel.FestivalEnd, dbModel.FestivalEnd);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var entity = TicketSeeds.RockForPeopleOne;
            entity.User = UserSeeds.MilosZeman;
            entity.Festival = FestivalSeeds.RFP;
            var updatedModel = TicketMapper.TicketToTicketDetailModel(entity);
            updatedModel.Type = TicketType.Vip;
            updatedModel.Length = TicketLength.FestivalWide;    // Same value as seed
            updatedModel.PriceAmount = Convert.ToDecimal(3000);
            // updatedModel.PriceCurrency = Currency.Czk;       // Unchanged seed value
            updatedModel.StartDate = new DateTime(2021, 06, 10, 15, 00, 00);
            updatedModel.UserId = UserSeeds.JohnDoe.Id;
            updatedModel.UserEmail = UserSeeds.JohnDoe.Email;
            updatedModel.UserName = UserSeeds.JohnDoe.Name;
            updatedModel.FestivalId = FestivalSeeds.Pohoda.Id;
            updatedModel.FestivalTitle = FestivalSeeds.Pohoda.Title;
            updatedModel.FestivalStart = FestivalSeeds.Pohoda.Start;
            updatedModel.FestivalEnd = FestivalSeeds.Pohoda.End;

            var dbModel = _ticketRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.Type, dbModel.Type);
            Assert.Equal(updatedModel.Length, dbModel.Length);
            Assert.Equal(updatedModel.PriceAmount, dbModel.PriceAmount);
            Assert.Equal(updatedModel.PriceCurrency, dbModel.PriceCurrency);
            Assert.Equal(updatedModel.StartDate, dbModel.StartDate);
            Assert.Equal(updatedModel.UserId, dbModel.UserId);
            Assert.Equal(updatedModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(updatedModel.UserName, dbModel.UserName);
            Assert.Equal(updatedModel.FestivalId, dbModel.FestivalId);
            Assert.Equal(updatedModel.FestivalTitle, dbModel.FestivalTitle);
            Assert.Equal(updatedModel.FestivalStart, dbModel.FestivalStart);
            Assert.Equal(updatedModel.FestivalEnd, dbModel.FestivalEnd);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Tickets.Find(id));

            _ticketRepositorySUT.Delete(id);

            Assert.Null(dbx.Tickets.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = TicketSeeds.Pohoda.Id;

            _ticketRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Tickets.Find(id));
        }

        // Extended tests
        [Fact]
        public void GetAll()
        {
            var entityPohoda = TicketSeeds.Pohoda;
            entityPohoda.User = UserSeeds.MilosZeman;
            entityPohoda.Festival = FestivalSeeds.Pohoda;
            var entityRFPOne = TicketSeeds.RockForPeopleOne;
            entityRFPOne.User = UserSeeds.MilosZeman;
            entityRFPOne.Festival = FestivalSeeds.RFP;
            var entityRFPTwo = TicketSeeds.RockForPeopleTwo;
            entityRFPTwo.User = UserSeeds.JohnDoe;
            entityRFPTwo.Festival = FestivalSeeds.RFP;
            var Pohoda = TicketMapper.TicketToTicketListModel(entityPohoda);
            var RFPOne = TicketMapper.TicketToTicketListModel(entityRFPOne);
            var RFPTwo = TicketMapper.TicketToTicketListModel(entityRFPTwo);

            var dbList = _ticketRepositorySUT.GetAll();

            var dbPohoda = dbList.Single(ticket => ticket.Id == Pohoda.Id);
            var dbRFPOne = dbList.Single(ticket => ticket.Id == RFPOne.Id);
            var dbRFPTwo = dbList.Single(ticket => ticket.Id == RFPTwo.Id);
            Assert.Equal(Pohoda.Type, dbPohoda.Type);
            Assert.Equal(Pohoda.Length, dbPohoda.Length);
            Assert.Equal(Pohoda.PriceAmount, dbPohoda.PriceAmount);
            Assert.Equal(Pohoda.PriceCurrency, dbPohoda.PriceCurrency);
            Assert.Equal(Pohoda.StartDate, dbPohoda.StartDate);
            Assert.Equal(Pohoda.UserId, dbPohoda.UserId);
            Assert.Equal(Pohoda.UserEmail, dbPohoda.UserEmail);
            Assert.Equal(Pohoda.UserName, dbPohoda.UserName);
            Assert.Equal(Pohoda.FestivalId, dbPohoda.FestivalId);
            Assert.Equal(Pohoda.FestivalTitle, dbPohoda.FestivalTitle);
            Assert.Equal(Pohoda.FestivalStart, dbPohoda.FestivalStart);
            Assert.Equal(Pohoda.FestivalEnd, dbPohoda.FestivalEnd);
            Assert.Equal(RFPOne.Type, dbRFPOne.Type);
            Assert.Equal(RFPOne.Length, dbRFPOne.Length);
            Assert.Equal(RFPOne.PriceAmount, dbRFPOne.PriceAmount);
            Assert.Equal(RFPOne.PriceCurrency, dbRFPOne.PriceCurrency);
            Assert.Equal(RFPOne.StartDate, dbRFPOne.StartDate);
            Assert.Equal(RFPOne.UserId, dbRFPOne.UserId);
            Assert.Equal(RFPOne.UserEmail, dbRFPOne.UserEmail);
            Assert.Equal(RFPOne.UserName, dbRFPOne.UserName);
            Assert.Equal(RFPOne.FestivalId, dbRFPOne.FestivalId);
            Assert.Equal(RFPOne.FestivalTitle, dbRFPOne.FestivalTitle);
            Assert.Equal(RFPOne.FestivalStart, dbRFPOne.FestivalStart);
            Assert.Equal(RFPOne.FestivalEnd, dbRFPOne.FestivalEnd);
            Assert.Equal(RFPTwo.Type, dbRFPTwo.Type);
            Assert.Equal(RFPTwo.Length, dbRFPTwo.Length);
            Assert.Equal(RFPTwo.PriceAmount, dbRFPTwo.PriceAmount);
            Assert.Equal(RFPTwo.PriceCurrency, dbRFPTwo.PriceCurrency);
            Assert.Equal(RFPTwo.StartDate, dbRFPTwo.StartDate);
            Assert.Equal(RFPTwo.UserId, dbRFPTwo.UserId);
            Assert.Equal(RFPTwo.UserEmail, dbRFPTwo.UserEmail);
            Assert.Equal(RFPTwo.UserName, dbRFPTwo.UserName);
            Assert.Equal(RFPTwo.FestivalId, dbRFPTwo.FestivalId);
            Assert.Equal(RFPTwo.FestivalTitle, dbRFPTwo.FestivalTitle);
            Assert.Equal(RFPTwo.FestivalStart, dbRFPTwo.FestivalStart);
            Assert.Equal(RFPTwo.FestivalEnd, dbRFPTwo.FestivalEnd);
        }

        [Fact]
        public void GetByUser()
        {
            var entityPohoda = TicketSeeds.Pohoda;
            entityPohoda.User = UserSeeds.MilosZeman;
            entityPohoda.Festival = FestivalSeeds.Pohoda;
            var entityRFP = TicketSeeds.RockForPeopleOne;
            entityRFP.User = UserSeeds.MilosZeman;
            entityRFP.Festival = FestivalSeeds.RFP;
            var RFP = TicketMapper.TicketToTicketListModel(entityRFP);
            var Pohoda = TicketMapper.TicketToTicketListModel(entityPohoda);

            var dbList = _ticketRepositorySUT.GetByUser(UserSeeds.MilosZeman.Id);

            var dbRFP = dbList.Single(ticket => ticket.Id == RFP.Id);
            var dbPohoda = dbList.Single(ticket => ticket.Id == Pohoda.Id);
            Assert.Equal(RFP.Type, dbRFP.Type);
            Assert.Equal(RFP.Length, dbRFP.Length);
            Assert.Equal(RFP.PriceAmount, dbRFP.PriceAmount);
            Assert.Equal(RFP.PriceCurrency, dbRFP.PriceCurrency);
            Assert.Equal(RFP.StartDate, dbRFP.StartDate);
            Assert.Equal(RFP.UserId, dbRFP.UserId);
            Assert.Equal(RFP.UserEmail, dbRFP.UserEmail);
            Assert.Equal(RFP.UserName, dbRFP.UserName);
            Assert.Equal(RFP.FestivalId, dbRFP.FestivalId);
            Assert.Equal(RFP.FestivalTitle, dbRFP.FestivalTitle);
            Assert.Equal(RFP.FestivalStart, dbRFP.FestivalStart);
            Assert.Equal(RFP.FestivalEnd, dbRFP.FestivalEnd);
            Assert.Equal(Pohoda.Type, dbPohoda.Type);
            Assert.Equal(Pohoda.Length, dbPohoda.Length);
            Assert.Equal(Pohoda.PriceAmount, dbPohoda.PriceAmount);
            Assert.Equal(Pohoda.PriceCurrency, dbPohoda.PriceCurrency);
            Assert.Equal(Pohoda.StartDate, dbPohoda.StartDate);
            Assert.Equal(Pohoda.UserId, dbPohoda.UserId);
            Assert.Equal(Pohoda.UserEmail, dbPohoda.UserEmail);
            Assert.Equal(Pohoda.UserName, dbPohoda.UserName);
            Assert.Equal(Pohoda.FestivalId, dbPohoda.FestivalId);
            Assert.Equal(Pohoda.FestivalTitle, dbPohoda.FestivalTitle);
            Assert.Equal(Pohoda.FestivalStart, dbPohoda.FestivalStart);
            Assert.Equal(Pohoda.FestivalEnd, dbPohoda.FestivalEnd);
        }

        [Fact]
        public void GetByFestival()
        {
            var entityRFPOne = TicketSeeds.RockForPeopleOne;
            entityRFPOne.User = UserSeeds.MilosZeman;
            entityRFPOne.Festival = FestivalSeeds.RFP;
            var entityRFPTwo = TicketSeeds.RockForPeopleTwo;
            entityRFPTwo.User = UserSeeds.JohnDoe;
            entityRFPTwo.Festival = FestivalSeeds.RFP;
            var RFPOne = TicketMapper.TicketToTicketListModel(entityRFPOne);
            var RFPTwo = TicketMapper.TicketToTicketListModel(entityRFPTwo);

            var dbList = _ticketRepositorySUT.GetByFestival(FestivalSeeds.RFP.Id);

            var dbRFPOne = dbList.Single(ticket => ticket.Id == RFPOne.Id);
            var dbRFPTwo = dbList.Single(ticket => ticket.Id == RFPTwo.Id);
            Assert.Equal(RFPOne.Type, dbRFPOne.Type);
            Assert.Equal(RFPOne.Length, dbRFPOne.Length);
            Assert.Equal(RFPOne.PriceAmount, dbRFPOne.PriceAmount);
            Assert.Equal(RFPOne.PriceCurrency, dbRFPOne.PriceCurrency);
            Assert.Equal(RFPOne.StartDate, dbRFPOne.StartDate);
            Assert.Equal(RFPOne.UserId, dbRFPOne.UserId);
            Assert.Equal(RFPOne.UserEmail, dbRFPOne.UserEmail);
            Assert.Equal(RFPOne.UserName, dbRFPOne.UserName);
            Assert.Equal(RFPOne.FestivalId, dbRFPOne.FestivalId);
            Assert.Equal(RFPOne.FestivalTitle, dbRFPOne.FestivalTitle);
            Assert.Equal(RFPOne.FestivalStart, dbRFPOne.FestivalStart);
            Assert.Equal(RFPOne.FestivalEnd, dbRFPOne.FestivalEnd);
            Assert.Equal(RFPTwo.Type, dbRFPTwo.Type);
            Assert.Equal(RFPTwo.Length, dbRFPTwo.Length);
            Assert.Equal(RFPTwo.PriceAmount, dbRFPTwo.PriceAmount);
            Assert.Equal(RFPTwo.PriceCurrency, dbRFPTwo.PriceCurrency);
            Assert.Equal(RFPTwo.StartDate, dbRFPTwo.StartDate);
            Assert.Equal(RFPTwo.UserId, dbRFPTwo.UserId);
            Assert.Equal(RFPTwo.UserEmail, dbRFPTwo.UserEmail);
            Assert.Equal(RFPTwo.UserName, dbRFPTwo.UserName);
            Assert.Equal(RFPTwo.FestivalId, dbRFPTwo.FestivalId);
            Assert.Equal(RFPTwo.FestivalTitle, dbRFPTwo.FestivalTitle);
            Assert.Equal(RFPTwo.FestivalStart, dbRFPTwo.FestivalStart);
            Assert.Equal(RFPTwo.FestivalEnd, dbRFPTwo.FestivalEnd);
        }
    }
}
