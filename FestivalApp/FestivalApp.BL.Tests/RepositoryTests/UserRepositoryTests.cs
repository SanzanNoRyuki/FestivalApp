using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{

    public sealed class UserRepositoryTests : IDisposable
    {
        // Setup
        private readonly UserRepository _userRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public UserRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(UserRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _userRepositorySUT = new UserRepository(_dbContextFactory);
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
            Assert.Null(dbx.Users.Find(id));

            var dbModel = _userRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var seedModel = UserMapper.UserToUserDetailModel(UserSeeds.JohnDoe);

            var dbModel = _userRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Email, dbModel.Email);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.PhoneNumber, dbModel.PhoneNumber);
            Assert.Equal(seedModel.AddressId, dbModel.AddressId);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new UserDetailModel
            {
                Email = "e@mail.com",
                Name = "Jozko Mrkvicka",
                PhoneNumber = "+42 987 654 321",
                AddressId = AddressSeeds.PragueCastle.Id
            };

            var dbModel = _userRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.Email, dbModel.Email);
            Assert.Equal(newModel.Name, dbModel.Name);
            Assert.Equal(newModel.PhoneNumber, dbModel.PhoneNumber);
            Assert.Equal(newModel.AddressId, dbModel.AddressId);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var updatedModel = UserMapper.UserToUserDetailModel(UserSeeds.MilosZeman);
            // updatedModel.Email = "e@mail.com";         // Unchanged seed value
            updatedModel.Name = "Jozko Mrkvicka";
            updatedModel.PhoneNumber = "+42 000 000 000"; // Same value as seed 
            updatedModel.AddressId = AddressSeeds.PragueCastle.Id;

            var dbModel = _userRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.Email, dbModel.Email);
            Assert.Equal(updatedModel.Name, dbModel.Name);
            Assert.Equal(updatedModel.PhoneNumber, dbModel.PhoneNumber);
            Assert.Equal(updatedModel.AddressId, dbModel.AddressId);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Users.Find(id));

            _userRepositorySUT.Delete(id);

            Assert.Null(dbx.Users.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = UserSeeds.MilosZeman.Id;

            _userRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Users.Find(id));
        }

        // Extended tests
        [Fact]
        public void GetAll()
        {
            var JohnDoe = UserMapper.UserToUserListModel(UserSeeds.JohnDoe);
            var MilosZeman = UserMapper.UserToUserListModel(UserSeeds.MilosZeman);

            var dbList = _userRepositorySUT.GetAll();

            var dbJohnDoe = dbList.Single(user => user.Id == JohnDoe.Id);
            var dbMilosZeman = dbList.Single(user => user.Id == MilosZeman.Id);
            Assert.Equal(JohnDoe.Email, dbJohnDoe.Email);
            Assert.Equal(JohnDoe.Name, dbJohnDoe.Name);
            Assert.Equal(MilosZeman.Email, dbMilosZeman.Email);
            Assert.Equal(MilosZeman.Name, dbMilosZeman.Name);
        }

        [Fact]
        public void GetByEmail_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            string email = "test@mail.com";
            Assert.Null(dbx.Users.SingleOrDefault(user => user.Email == email));

            var dbModel = _userRepositorySUT.GetByEmail(email);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByEmail_WithExistingItem()
        {
            var seedModel = UserMapper.UserToUserDetailModel(UserSeeds.JohnDoe);

            var dbModel = _userRepositorySUT.GetByEmail(seedModel.Email);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Email, dbModel.Email);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.PhoneNumber, dbModel.PhoneNumber);
            Assert.Equal(seedModel.AddressId, dbModel.AddressId);
        }

        [Fact]
        public void GetByFestival_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Festivals.Find(id));

            var dbModel = _userRepositorySUT.GetByFestival(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByFestival_WithExistingItem()
        {
            var seedModel = UserMapper.UserToUserDetailModel(UserSeeds.JohnDoe);

            var dbModel = _userRepositorySUT.GetByFestival(FestivalSeeds.Pohoda.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Email, dbModel.Email);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.PhoneNumber, dbModel.PhoneNumber);
            Assert.Equal(seedModel.AddressId, dbModel.AddressId);
        }

        [Fact]
        public void GetByTicket_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Tickets.Find(id));

            var dbModel = _userRepositorySUT.GetByTicket(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByTicket_WithExistingItem()
        {
            var seedModel = UserMapper.UserToUserDetailModel(UserSeeds.MilosZeman);

            var dbModel = _userRepositorySUT.GetByTicket(TicketSeeds.RockForPeopleOne.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Email, dbModel.Email);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.PhoneNumber, dbModel.PhoneNumber);
            Assert.Equal(seedModel.AddressId, dbModel.AddressId);
        }
    }
}
