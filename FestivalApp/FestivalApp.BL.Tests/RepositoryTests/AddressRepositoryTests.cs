using System;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{
    public sealed class AddressRepositoryTests : IDisposable
    {
        // Setup
        private readonly AddressRepository _addressRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public AddressRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(AddressRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _addressRepositorySUT = new AddressRepository(_dbContextFactory);
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
            Assert.Null(dbx.Addresses.Find(id));

            var dbModel = _addressRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var seedModel = AddressMapper.AddressToAddressDetailModel(AddressSeeds.Bozetechova);

            var dbModel = _addressRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.AddressLine1, dbModel.AddressLine1);
            Assert.Equal(seedModel.AddressLine2, dbModel.AddressLine2);
            Assert.Equal(seedModel.City, dbModel.City);
            Assert.Equal(seedModel.State, dbModel.State);
            Assert.Equal(seedModel.Country, dbModel.Country);
            Assert.Equal(seedModel.PostalCode, dbModel.PostalCode);
            Assert.Equal(seedModel.UserId, dbModel.UserId);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new AddressDetailModel
            {
                AddressLine1 = "Nalepkova",
                AddressLine2 = "10",
                City = "Horka",
                State = "Popradsky",
                Country = "Slovakia",
                PostalCode = "059 21",
                UserId = UserSeeds.JohnDoe.Id
            };

            var dbModel = _addressRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.AddressLine1, dbModel.AddressLine1);
            Assert.Equal(newModel.AddressLine2, dbModel.AddressLine2);
            Assert.Equal(newModel.City, dbModel.City);
            Assert.Equal(newModel.State, dbModel.State);
            Assert.Equal(newModel.Country, dbModel.Country);
            Assert.Equal(newModel.PostalCode, dbModel.PostalCode);
            Assert.Equal(newModel.UserId, dbModel.UserId);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var updatedModel = AddressMapper.AddressToAddressDetailModel(AddressSeeds.Bozetechova);
            updatedModel.AddressLine1 = "Nalepkova";
            updatedModel.AddressLine2 = "10";
            updatedModel.City = "Horka";
            updatedModel.State = "Popradsky";
            updatedModel.Country = "Czechia";       // Same value as seed
            // updatedModel.PostalCode = "059 21";  // Unchanged seed value
            updatedModel.UserId = UserSeeds.MilosZeman.Id;

            var dbModel = _addressRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.AddressLine1, dbModel.AddressLine1);
            Assert.Equal(updatedModel.AddressLine2, dbModel.AddressLine2);
            Assert.Equal(updatedModel.City, dbModel.City);
            Assert.Equal(updatedModel.State, dbModel.State);
            Assert.Equal(updatedModel.Country, dbModel.Country);
            Assert.Equal(updatedModel.PostalCode, dbModel.PostalCode);
            Assert.Equal(updatedModel.UserId, dbModel.UserId);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Addresses.Find(id));

            _addressRepositorySUT.Delete(id);

            Assert.Null(dbx.Addresses.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = AddressSeeds.Bozetechova.Id;

            _addressRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Addresses.Find(id));
        }

        // Extended tests
        [Fact]
        public void GetByUser_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Users.Find(id));

            var dbModel = _addressRepositorySUT.GetByUser(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByUser_WithExistingItem()
        {
            var seedModel = AddressMapper.AddressToAddressDetailModel(AddressSeeds.Bozetechova);

            var dbModel = _addressRepositorySUT.GetByUser(AddressSeeds.Bozetechova.UserId);

            Assert.Equal(seedModel.AddressLine1, dbModel.AddressLine1);
            Assert.Equal(seedModel.AddressLine2, dbModel.AddressLine2);
            Assert.Equal(seedModel.City, dbModel.City);
            Assert.Equal(seedModel.State, dbModel.State);
            Assert.Equal(seedModel.Country, dbModel.Country);
            Assert.Equal(seedModel.PostalCode, dbModel.PostalCode);
            Assert.Equal(seedModel.UserId, dbModel.UserId);
        }
    }
}
