using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{

    public sealed class FestivalRepositoryTests : IDisposable
    {
        // Setup
        private readonly FestivalRepository _festivalRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public FestivalRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(FestivalRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _festivalRepositorySUT = new FestivalRepository(_dbContextFactory);
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
            Assert.Null(dbx.Festivals.Find(id));

            var dbModel = _festivalRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var entity = FestivalSeeds.Pohoda;
            entity.User = UserSeeds.JohnDoe;
            var seedModel = FestivalMapper.FestivalToFestivalDetailModel(entity);

            var dbModel = _festivalRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Title, dbModel.Title);
            Assert.Equal(seedModel.Start, dbModel.Start);
            Assert.Equal(seedModel.End, dbModel.End);
            Assert.Equal(seedModel.Capacity, dbModel.Capacity);
            Assert.Equal(seedModel.UserId, dbModel.UserId);
            Assert.Equal(seedModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(seedModel.UserName, dbModel.UserName);
            Assert.Equal(seedModel.UserAddressId, dbModel.UserAddressId);
            Assert.Equal(seedModel.UserPhoneNumber, dbModel.UserPhoneNumber);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new FestivalDetailModel
            {
                Title = "Siget",
                Start = new DateTime(2022, 05, 10, 15, 00, 00),
                End = new DateTime(2022, 05, 15, 23, 00, 00),
                Capacity = 30000,
                UserId = UserSeeds.MilosZeman.Id,
                UserEmail = UserSeeds.MilosZeman.Email,
                UserName = UserSeeds.MilosZeman.Name,
                UserAddressId = UserSeeds.MilosZeman.AddressId,
                UserPhoneNumber = UserSeeds.MilosZeman.PhoneNumber
            };

            var dbModel = _festivalRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.Title, dbModel.Title);
            Assert.Equal(newModel.Start, dbModel.Start);
            Assert.Equal(newModel.End, dbModel.End);
            Assert.Equal(newModel.Capacity, dbModel.Capacity);
            Assert.Equal(newModel.UserId, dbModel.UserId);
            Assert.Equal(newModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(newModel.UserName, dbModel.UserName);
            Assert.Equal(newModel.UserAddressId, dbModel.UserAddressId);
            Assert.Equal(newModel.UserPhoneNumber, dbModel.UserPhoneNumber);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var entity = FestivalSeeds.Pohoda;
            entity.User = UserSeeds.JohnDoe;
            var updatedModel = FestivalMapper.FestivalToFestivalDetailModel(entity);
            // updatedModel.Title = "Siget";  // Unchanged seed value
            updatedModel.Start = new DateTime(2022, 05, 10, 15, 00, 00);
            updatedModel.End = new DateTime(2022, 05, 15, 23, 00, 00);
            updatedModel.Capacity = 10000;    // Same value as seed
            updatedModel.UserId = UserSeeds.MilosZeman.Id;
            updatedModel.UserEmail = UserSeeds.MilosZeman.Email;
            updatedModel.UserName = UserSeeds.MilosZeman.Name;
            updatedModel.UserAddressId = UserSeeds.MilosZeman.AddressId;
            updatedModel.UserPhoneNumber = UserSeeds.MilosZeman.PhoneNumber;

            var dbModel = _festivalRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.Title, dbModel.Title);
            Assert.Equal(updatedModel.Start, dbModel.Start);
            Assert.Equal(updatedModel.End, dbModel.End);
            Assert.Equal(updatedModel.Capacity, dbModel.Capacity);
            Assert.Equal(updatedModel.UserId, dbModel.UserId);
            Assert.Equal(updatedModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(updatedModel.UserName, dbModel.UserName);
            Assert.Equal(updatedModel.UserAddressId, dbModel.UserAddressId);
            Assert.Equal(updatedModel.UserPhoneNumber, dbModel.UserPhoneNumber);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Festivals.Find(id));

            _festivalRepositorySUT.Delete(id);

            Assert.Null(dbx.Festivals.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = FestivalSeeds.Pohoda.Id;

            _festivalRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Festivals.Find(id));
        }

        // Extended tests
        [Fact]
        public void GetAll()
        {
            var entityPohoda = FestivalSeeds.Pohoda;
            entityPohoda.User = UserSeeds.JohnDoe;
            var entityRFP = FestivalSeeds.RFP;
            entityRFP.User = UserSeeds.JohnDoe;
            var Pohoda = FestivalMapper.FestivalToFestivalListModel(entityPohoda);
            var RFP = FestivalMapper.FestivalToFestivalListModel(entityPohoda);

            var dbList = _festivalRepositorySUT.GetAll();

            var dbPohoda = dbList.Single(festival => festival.Id == Pohoda.Id);
            var dbRFP = dbList.Single(festival => festival.Id == RFP.Id);
            Assert.Equal(Pohoda.Title, dbPohoda.Title);
            Assert.Equal(Pohoda.Start, dbPohoda.Start);
            Assert.Equal(Pohoda.End, dbPohoda.End);
            Assert.Equal(Pohoda.UserId, dbPohoda.UserId);
            Assert.Equal(Pohoda.UserEmail, dbPohoda.UserEmail);
            Assert.Equal(Pohoda.UserName, dbPohoda.UserName);
            Assert.Equal(RFP.Title, dbRFP.Title);
            Assert.Equal(RFP.Start, dbRFP.Start);
            Assert.Equal(RFP.End, dbRFP.End);
            Assert.Equal(RFP.UserId, dbRFP.UserId);
            Assert.Equal(RFP.UserEmail, dbRFP.UserEmail);
            Assert.Equal(RFP.UserName, dbRFP.UserName);
        }

        [Fact]
        public void GetByOwner()
        {
            var entityPohoda = FestivalSeeds.Pohoda;
            entityPohoda.User = UserSeeds.JohnDoe;
            var Pohoda = FestivalMapper.FestivalToFestivalListModel(entityPohoda);
            var entityRFP = FestivalSeeds.RFP;
            entityRFP.User = UserSeeds.JohnDoe;
            var RFP = FestivalMapper.FestivalToFestivalListModel(entityPohoda);

            var dbList = _festivalRepositorySUT.GetByOwner(UserSeeds.JohnDoe.Id);

            var dbPohoda = dbList.Single(festival => festival.Id == Pohoda.Id);
            var dbRFP = dbList.Single(festival => festival.Id == RFP.Id);
            Assert.Equal(Pohoda.Title, dbPohoda.Title);
            Assert.Equal(Pohoda.Start, dbPohoda.Start);
            Assert.Equal(Pohoda.End, dbPohoda.End);
            Assert.Equal(Pohoda.UserId, dbPohoda.UserId);
            Assert.Equal(Pohoda.UserEmail, dbPohoda.UserEmail);
            Assert.Equal(Pohoda.UserName, dbPohoda.UserName);
            Assert.Equal(RFP.Title, dbRFP.Title);
            Assert.Equal(RFP.Start, dbRFP.Start);
            Assert.Equal(RFP.End, dbRFP.End);
            Assert.Equal(RFP.UserId, dbRFP.UserId);
            Assert.Equal(RFP.UserEmail, dbRFP.UserEmail);
            Assert.Equal(RFP.UserName, dbRFP.UserName);
        }

        [Fact]
        public void GetByStage_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Stages.Find(id));

            var dbModel = _festivalRepositorySUT.GetByStage(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByStage_WithExistingItem()
        {
            var entity = FestivalSeeds.RFP;
            entity.User = UserSeeds.JohnDoe;
            var seedModel = FestivalMapper.FestivalToFestivalDetailModel(entity);

            var dbModel = _festivalRepositorySUT.GetByStage(StageSeeds.StageEastRFP.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Title, dbModel.Title);
            Assert.Equal(seedModel.Start, dbModel.Start);
            Assert.Equal(seedModel.End, dbModel.End);
            Assert.Equal(seedModel.Capacity, dbModel.Capacity);
            Assert.Equal(seedModel.UserId, dbModel.UserId);
            Assert.Equal(seedModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(seedModel.UserName, dbModel.UserName);
            Assert.Equal(seedModel.UserAddressId, dbModel.UserAddressId);
            Assert.Equal(seedModel.UserPhoneNumber, dbModel.UserPhoneNumber);
        }

        [Fact]
        public void GetByTicket_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Tickets.Find(id));

            var dbModel = _festivalRepositorySUT.GetByTicket(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByTicket_WithExistingItem()
        {
            var entity = FestivalSeeds.RFP;
            entity.User = UserSeeds.JohnDoe;
            var seedModel = FestivalMapper.FestivalToFestivalDetailModel(entity);

            var dbModel = _festivalRepositorySUT.GetByTicket(TicketSeeds.RockForPeopleOne.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Title, dbModel.Title);
            Assert.Equal(seedModel.Start, dbModel.Start);
            Assert.Equal(seedModel.End, dbModel.End);
            Assert.Equal(seedModel.Capacity, dbModel.Capacity);
            Assert.Equal(seedModel.UserId, dbModel.UserId);
            Assert.Equal(seedModel.UserEmail, dbModel.UserEmail);
            Assert.Equal(seedModel.UserName, dbModel.UserName);
            Assert.Equal(seedModel.UserAddressId, dbModel.UserAddressId);
            Assert.Equal(seedModel.UserPhoneNumber, dbModel.UserPhoneNumber);
        }
    }
}
