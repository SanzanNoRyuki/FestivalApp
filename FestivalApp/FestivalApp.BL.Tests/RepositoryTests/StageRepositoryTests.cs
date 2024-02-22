using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{

    public sealed class StageRepositoryTests : IDisposable
    {
        // Setup
        private readonly StageRepository _stageRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public StageRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(StageRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _stageRepositorySUT = new StageRepository(_dbContextFactory);
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
            Assert.Null(dbx.Stages.Find(id));

            var dbModel = _stageRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var entity = StageSeeds.StageEastRFP;
            entity.Festival = FestivalSeeds.RFP;
            var seedModel = StageMapper.StageToStageDetailModel(entity);

            var dbModel = _stageRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.Description, dbModel.Description);
            Assert.Equal(seedModel.PhotoPath, dbModel.PhotoPath);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new StageDetailModel
            {
                Name = "StageWest",
                Description = "This is description",
                PhotoPath = "img/photo.png",

                FestivalId = FestivalSeeds.Pohoda.Id,
                FestivalTitle = FestivalSeeds.Pohoda.Title,
                FestivalStart = FestivalSeeds.Pohoda.Start,
                FestivalEnd = FestivalSeeds.Pohoda.End,
                FestivalCapacity = FestivalSeeds.Pohoda.Capacity
            };

            var dbModel = _stageRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.Name, dbModel.Name);
            Assert.Equal(newModel.Description, dbModel.Description);
            Assert.Equal(newModel.PhotoPath, dbModel.PhotoPath);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var entity = StageSeeds.StageWestRFP;
            entity.Festival = FestivalSeeds.RFP;
            var updatedModel = StageMapper.StageToStageDetailModel(entity);
            updatedModel.Name = "Stage West";             // Same value as seed 
            updatedModel.Description = "This is description";
            // updatedModel.PhotoPath = "img/photo.png";  // Unchanged seed value

            updatedModel.FestivalId = FestivalSeeds.Pohoda.Id;
            updatedModel.FestivalTitle = FestivalSeeds.Pohoda.Title;
            updatedModel.FestivalStart = FestivalSeeds.Pohoda.Start;
            updatedModel.FestivalEnd = FestivalSeeds.Pohoda.End;
            updatedModel.FestivalCapacity = FestivalSeeds.Pohoda.Capacity;

            var dbModel = _stageRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.Name, dbModel.Name);
            Assert.Equal(updatedModel.Description, dbModel.Description);
            Assert.Equal(updatedModel.PhotoPath, dbModel.PhotoPath);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Stages.Find(id));

            _stageRepositorySUT.Delete(id);

            Assert.Null(dbx.Stages.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = StageSeeds.StageMainPohoda.Id;

            _stageRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Stages.Find(id));
        }

        // Extended tests
        [Fact]
        public void GetAll()
        {
            var entityStageWestRFP = StageSeeds.StageWestRFP;
            entityStageWestRFP.Festival = FestivalSeeds.RFP;
            var entityStageEastRFP = StageSeeds.StageEastRFP;
            entityStageEastRFP.Festival = FestivalSeeds.RFP;
            var entityStageMainPohoda = StageSeeds.StageMainPohoda;
            entityStageMainPohoda.Festival = FestivalSeeds.Pohoda;
            var StageWestRFP = StageMapper.StageToStageDetailModel(entityStageWestRFP);
            var StageEastRFP = StageMapper.StageToStageDetailModel(entityStageEastRFP);
            var StageMainPohoda = StageMapper.StageToStageDetailModel(entityStageMainPohoda);

            var dbList = _stageRepositorySUT.GetAll();

            var dbStageWestRFP = dbList.Single(stage => stage.Id == StageWestRFP.Id);
            var dbStageEastRFP = dbList.Single(stage => stage.Id == StageEastRFP.Id);
            var dbStageMainPohoda = dbList.Single(stage => stage.Id == StageMainPohoda.Id);

            Assert.Equal(StageWestRFP.Name, dbStageWestRFP.Name);
            Assert.Equal(StageEastRFP.Name, dbStageEastRFP.Name);
            Assert.Equal(dbStageMainPohoda.Name, dbStageMainPohoda.Name);
        }

        [Fact]
        public void GetByFestival()
        {
            var entityStageWestRFP = StageSeeds.StageWestRFP;
            entityStageWestRFP.Festival = FestivalSeeds.RFP;
            var entityStageEastRFP = StageSeeds.StageEastRFP;
            entityStageEastRFP.Festival = FestivalSeeds.RFP;
            var StageWestRFP = StageMapper.StageToStageDetailModel(entityStageWestRFP);
            var StageEastRFP = StageMapper.StageToStageDetailModel(entityStageEastRFP);

            var dbList = _stageRepositorySUT.GetByFestival(FestivalSeeds.RFP.Id);

            var dbStageWestRFP = dbList.Single(stage => stage.Id == StageWestRFP.Id);
            var dbStageEastRFP = dbList.Single(stage => stage.Id == StageEastRFP.Id);
            Assert.Equal(StageWestRFP.Name, dbStageWestRFP.Name);
            Assert.Equal(StageEastRFP.Name, dbStageEastRFP.Name);
        }

        [Fact]
        public void GetByShow_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Shows.Find(id));

            var dbModel = _stageRepositorySUT.GetByShow(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByShow_WithExistingItem()
        {
            var entity = StageSeeds.StageMainPohoda;
            entity.Festival = FestivalSeeds.Pohoda;
            var seedModel = StageMapper.StageToStageDetailModel(entity);

            var dbModel = _stageRepositorySUT.GetByShow(ShowSeeds.FreddyPohoda.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.Description, dbModel.Description);
            Assert.Equal(seedModel.PhotoPath, dbModel.PhotoPath);
        }
    }
}
