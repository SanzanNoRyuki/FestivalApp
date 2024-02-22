using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{

    public sealed class ShowRepositoryTests : IDisposable
    {
        // Setup
        private readonly ShowRepository _showRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public ShowRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(ShowRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _showRepositorySUT = new ShowRepository(_dbContextFactory);
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
            Assert.Null(dbx.Shows.Find(id));

            var dbModel = _showRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var entity = ShowSeeds.QueenRFP;
            entity.Artist = ArtistSeeds.Queen;
            entity.Stage = StageSeeds.StageEastRFP;
            var seedModel = ShowMapper.ShowToShowDetailModel(entity);

            var dbModel = _showRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.StartPlaying, dbModel.StartPlaying);
            Assert.Equal(seedModel.EndPlaying, dbModel.EndPlaying);
            Assert.Equal(seedModel.LengthOfPlaying, dbModel.LengthOfPlaying);
            Assert.Equal(seedModel.ArtistId, dbModel.ArtistId);
            Assert.Equal(seedModel.ArtistName, dbModel.ArtistName);
            Assert.Equal(seedModel.ArtistCountry, dbModel.ArtistCountry);
            Assert.Equal(seedModel.ArtistGenre, dbModel.ArtistGenre);
            Assert.Equal(seedModel.ArtistShortDescription, dbModel.ArtistShortDescription);
            Assert.Equal(seedModel.ArtistLongDescription, dbModel.ArtistLongDescription);
            Assert.Equal(seedModel.ArtistPhotoPath, dbModel.ArtistPhotoPath);
            Assert.Equal(seedModel.StageId, dbModel.StageId);
            Assert.Equal(seedModel.StageName, dbModel.StageName);
            Assert.Equal(seedModel.StageDescription, dbModel.StageDescription);
            Assert.Equal(seedModel.StagePhotoPath, dbModel.StagePhotoPath);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new ShowDetailModel
            {
                StartPlaying = new DateTime(2023, 06, 10, 18, 00, 00),
                EndPlaying = new DateTime(2023, 06, 10, 20, 00, 00),
                LengthOfPlaying = new TimeSpan(02, 00, 00),
                ArtistId = ArtistSeeds.Queen.Id,
                ArtistName = ArtistSeeds.Queen.Name,
                ArtistCountry = ArtistSeeds.Queen.Country,
                ArtistGenre = ArtistSeeds.Queen.Genre,
                ArtistShortDescription = ArtistSeeds.Queen.ShortDescription,
                ArtistLongDescription = ArtistSeeds.Queen.LongDescription,
                ArtistPhotoPath = ArtistSeeds.Queen.PhotoPath,
                StageId = StageSeeds.StageMainPohoda.Id,
                StageName = StageSeeds.StageMainPohoda.Name,
                StageDescription = StageSeeds.StageMainPohoda.Description,
                StagePhotoPath = StageSeeds.StageMainPohoda.PhotoPath
            };

            var dbModel = _showRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.StartPlaying, dbModel.StartPlaying);
            Assert.Equal(newModel.EndPlaying, dbModel.EndPlaying);
            Assert.Equal(newModel.LengthOfPlaying, dbModel.LengthOfPlaying);
            Assert.Equal(newModel.ArtistId, dbModel.ArtistId);
            Assert.Equal(newModel.ArtistName, dbModel.ArtistName);
            Assert.Equal(newModel.ArtistCountry, dbModel.ArtistCountry);
            Assert.Equal(newModel.ArtistGenre, dbModel.ArtistGenre);
            Assert.Equal(newModel.ArtistShortDescription, dbModel.ArtistShortDescription);
            Assert.Equal(newModel.ArtistLongDescription, dbModel.ArtistLongDescription);
            Assert.Equal(newModel.ArtistPhotoPath, dbModel.ArtistPhotoPath);
            Assert.Equal(newModel.StageId, dbModel.StageId);
            Assert.Equal(newModel.StageName, dbModel.StageName);
            Assert.Equal(newModel.StageDescription, dbModel.StageDescription);
            Assert.Equal(newModel.StagePhotoPath, dbModel.StagePhotoPath);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var entity = ShowSeeds.FreddyPohoda;
            entity.Artist = ArtistSeeds.FreddyMercury;
            entity.Stage = StageSeeds.StageMainPohoda;
            var updatedModel = ShowMapper.ShowToShowDetailModel(entity);

            updatedModel.StartPlaying = new DateTime(2020, 06, 10, 18, 00, 00);
            updatedModel.EndPlaying = new DateTime(2021, 07, 04, 18, 00, 00);  // Same value as seed
            // updatedModel.LengthOfPlaying = new TimeSpan(02, 00, 00);        // Unchanged seed value
            updatedModel.ArtistId = ArtistSeeds.Queen.Id;
            updatedModel.ArtistName = ArtistSeeds.Queen.Name;
            updatedModel.ArtistCountry = ArtistSeeds.Queen.Country;
            updatedModel.ArtistGenre = ArtistSeeds.Queen.Genre;
            updatedModel.ArtistShortDescription = ArtistSeeds.Queen.ShortDescription;
            updatedModel.ArtistLongDescription = ArtistSeeds.Queen.LongDescription;
            updatedModel.ArtistPhotoPath = ArtistSeeds.Queen.PhotoPath;
            updatedModel.StageId = StageSeeds.StageMainPohoda.Id;
            updatedModel.StageName = StageSeeds.StageMainPohoda.Name;
            updatedModel.StageDescription = StageSeeds.StageMainPohoda.Description;
            updatedModel.StagePhotoPath = StageSeeds.StageMainPohoda.PhotoPath;

            var dbModel = _showRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.StartPlaying, dbModel.StartPlaying);
            Assert.Equal(updatedModel.EndPlaying, dbModel.EndPlaying);
            Assert.Equal(updatedModel.LengthOfPlaying, dbModel.LengthOfPlaying);
            Assert.Equal(updatedModel.ArtistId, dbModel.ArtistId);
            Assert.Equal(updatedModel.ArtistName, dbModel.ArtistName);
            Assert.Equal(updatedModel.ArtistCountry, dbModel.ArtistCountry);
            Assert.Equal(updatedModel.ArtistGenre, dbModel.ArtistGenre);
            Assert.Equal(updatedModel.ArtistShortDescription, dbModel.ArtistShortDescription);
            Assert.Equal(updatedModel.ArtistLongDescription, dbModel.ArtistLongDescription);
            Assert.Equal(updatedModel.ArtistPhotoPath, dbModel.ArtistPhotoPath);
            Assert.Equal(updatedModel.StageId, dbModel.StageId);
            Assert.Equal(updatedModel.StageName, dbModel.StageName);
            Assert.Equal(updatedModel.StageDescription, dbModel.StageDescription);
            Assert.Equal(updatedModel.StagePhotoPath, dbModel.StagePhotoPath);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Shows.Find(id));

            _showRepositorySUT.Delete(id);

            Assert.Null(dbx.Shows.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = ShowSeeds.FreddyPohoda.Id;

            _showRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Shows.Find(id));
        }

        // Extended tests
        [Fact]
        public void CreateOrUpdate_CheckOverlap_OverlappingShows()
        {
            var newModel = new ShowDetailModel
            {
                StartPlaying = new DateTime(2021, 07, 04, 19, 00, 00),
                EndPlaying = new DateTime(2021, 07, 04, 20, 30, 00),
                LengthOfPlaying = new TimeSpan(01, 30, 00),
                ArtistId = ArtistSeeds.Queen.Id,
                ArtistName = ArtistSeeds.Queen.Name,
                ArtistCountry = ArtistSeeds.Queen.Country,
                ArtistGenre = ArtistSeeds.Queen.Genre,
                ArtistShortDescription = ArtistSeeds.Queen.ShortDescription,
                ArtistLongDescription = ArtistSeeds.Queen.LongDescription,
                ArtistPhotoPath = ArtistSeeds.Queen.PhotoPath,
                StageId = StageSeeds.StageMainPohoda.Id,    // Overlapping stage
                StageName = StageSeeds.StageMainPohoda.Name,
                StageDescription = StageSeeds.StageMainPohoda.Description,
                StagePhotoPath = StageSeeds.StageMainPohoda.PhotoPath
            };

            var dbModel = _showRepositorySUT.CreateOrUpdate(newModel);

            Assert.Null(dbModel);
        }

        [Fact]
        public void CreateOrUpdate_CheckOverlap_OverlappingArtists()
        {
            var newModel = new ShowDetailModel
            {
                StartPlaying = new DateTime(2021, 06, 10, 18, 00, 00),
                EndPlaying = new DateTime(2021, 06, 10, 21, 00, 00),
                LengthOfPlaying = new TimeSpan(03, 00, 00),
                ArtistId = ArtistSeeds.Queen.Id,    // Overlapping artist
                ArtistName = ArtistSeeds.Queen.Name,
                ArtistCountry = ArtistSeeds.Queen.Country,
                ArtistGenre = ArtistSeeds.Queen.Genre,
                ArtistShortDescription = ArtistSeeds.Queen.ShortDescription,
                ArtistLongDescription = ArtistSeeds.Queen.LongDescription,
                ArtistPhotoPath = ArtistSeeds.Queen.PhotoPath,
                StageId = StageSeeds.StageWestRFP.Id,
                StageName = StageSeeds.StageWestRFP.Name,
                StageDescription = StageSeeds.StageWestRFP.Description,
                StagePhotoPath = StageSeeds.StageWestRFP.PhotoPath
            };

            var dbModel = _showRepositorySUT.CreateOrUpdate(newModel);

            Assert.Null(dbModel);
        }

        [Fact]
        public void CreateOrUpdate_CheckOverlap_SameShowUpdate()
        {
            var entity = ShowSeeds.FreddyPohoda;    // SeededShow
            entity.Artist = ArtistSeeds.FreddyMercury;
            entity.Stage = StageSeeds.StageMainPohoda;
            var updatedModel = ShowMapper.ShowToShowDetailModel(entity);
            updatedModel.StartPlaying = new DateTime(2021, 07, 04, 18, 00, 00);
            updatedModel.EndPlaying = new DateTime(2021, 07, 04, 19, 30, 00);
            updatedModel.LengthOfPlaying = new TimeSpan(01, 30, 00);
            updatedModel.ArtistId = ArtistSeeds.Queen.Id;
            updatedModel.ArtistName = ArtistSeeds.Queen.Name;
            updatedModel.ArtistCountry = ArtistSeeds.Queen.Country;
            updatedModel.ArtistGenre = ArtistSeeds.Queen.Genre;
            updatedModel.ArtistShortDescription = ArtistSeeds.Queen.ShortDescription;
            updatedModel.ArtistLongDescription = ArtistSeeds.Queen.LongDescription;
            updatedModel.ArtistPhotoPath = ArtistSeeds.Queen.PhotoPath;

            var dbModel = _showRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.StartPlaying, dbModel.StartPlaying);
            Assert.Equal(updatedModel.EndPlaying, dbModel.EndPlaying);
            Assert.Equal(updatedModel.LengthOfPlaying, dbModel.LengthOfPlaying);
            Assert.Equal(updatedModel.ArtistId, dbModel.ArtistId);
            Assert.Equal(updatedModel.ArtistName, dbModel.ArtistName);
            Assert.Equal(updatedModel.ArtistCountry, dbModel.ArtistCountry);
            Assert.Equal(updatedModel.ArtistGenre, dbModel.ArtistGenre);
            Assert.Equal(updatedModel.ArtistShortDescription, dbModel.ArtistShortDescription);
            Assert.Equal(updatedModel.ArtistLongDescription, dbModel.ArtistLongDescription);
            Assert.Equal(updatedModel.ArtistPhotoPath, dbModel.ArtistPhotoPath);
            Assert.Equal(updatedModel.StageId, dbModel.StageId);
            Assert.Equal(updatedModel.StageName, dbModel.StageName);
            Assert.Equal(updatedModel.StageDescription, dbModel.StageDescription);
            Assert.Equal(updatedModel.StagePhotoPath, dbModel.StagePhotoPath);
        }

        [Fact]
        public void GetByStage()
        {
            var entityFreddyPohoda = ShowSeeds.FreddyPohoda;
            entityFreddyPohoda.Artist = ArtistSeeds.FreddyMercury;
            entityFreddyPohoda.Stage = StageSeeds.StageMainPohoda;
            var FreddyPohoda = ShowMapper.ShowToShowListModel(entityFreddyPohoda);

            var dbList = _showRepositorySUT.GetByStage(StageSeeds.StageMainPohoda.Id);

            var dbFreddyPohoda = dbList.Single(show => show.Id == FreddyPohoda.Id);
            Assert.Equal(FreddyPohoda.StartPlaying, dbFreddyPohoda.StartPlaying);
            Assert.Equal(FreddyPohoda.EndPlaying, dbFreddyPohoda.EndPlaying);
            Assert.Equal(FreddyPohoda.LengthOfPlaying, dbFreddyPohoda.LengthOfPlaying);
            Assert.Equal(FreddyPohoda.ArtistId, dbFreddyPohoda.ArtistId);
            Assert.Equal(FreddyPohoda.ArtistName, dbFreddyPohoda.ArtistName);
            Assert.Equal(FreddyPohoda.ArtistGenre, dbFreddyPohoda.ArtistGenre);
            Assert.Equal(FreddyPohoda.StageId, dbFreddyPohoda.StageId);
            Assert.Equal(FreddyPohoda.StageName, dbFreddyPohoda.StageName);
        }

        [Fact]
        public void GetByArtist()
        {
            var entityFreddyRFP = ShowSeeds.FreddyRFP;
            entityFreddyRFP.Artist = ArtistSeeds.FreddyMercury;
            entityFreddyRFP.Stage = StageSeeds.StageWestRFP;
            var entityFreddyPohoda = ShowSeeds.FreddyPohoda;
            entityFreddyPohoda.Artist = ArtistSeeds.FreddyMercury;
            entityFreddyPohoda.Stage = StageSeeds.StageMainPohoda;
            var FreddyRFP = ShowMapper.ShowToShowListModel(entityFreddyRFP);
            var FreddyPohoda = ShowMapper.ShowToShowListModel(entityFreddyPohoda);

            var dbList = _showRepositorySUT.GetByArtist(ArtistSeeds.FreddyMercury.Id);

            var dbFreddyRFP = dbList.Single(show => show.Id == FreddyRFP.Id);
            var dbFreddyPohoda = dbList.Single(show => show.Id == FreddyPohoda.Id);
            Assert.Equal(FreddyRFP.StartPlaying, dbFreddyRFP.StartPlaying);
            Assert.Equal(FreddyRFP.EndPlaying, dbFreddyRFP.EndPlaying);
            Assert.Equal(FreddyRFP.LengthOfPlaying, dbFreddyRFP.LengthOfPlaying);
            Assert.Equal(FreddyRFP.ArtistId, dbFreddyRFP.ArtistId);
            Assert.Equal(FreddyRFP.ArtistName, dbFreddyRFP.ArtistName);
            Assert.Equal(FreddyRFP.ArtistGenre, dbFreddyRFP.ArtistGenre);
            Assert.Equal(FreddyRFP.StageId, dbFreddyRFP.StageId);
            Assert.Equal(FreddyRFP.StageName, dbFreddyRFP.StageName);
            Assert.Equal(FreddyPohoda.StartPlaying, dbFreddyPohoda.StartPlaying);
            Assert.Equal(FreddyPohoda.EndPlaying, dbFreddyPohoda.EndPlaying);
            Assert.Equal(FreddyPohoda.LengthOfPlaying, dbFreddyPohoda.LengthOfPlaying);
            Assert.Equal(FreddyPohoda.ArtistId, dbFreddyPohoda.ArtistId);
            Assert.Equal(FreddyPohoda.ArtistName, dbFreddyPohoda.ArtistName);
            Assert.Equal(FreddyPohoda.ArtistGenre, dbFreddyPohoda.ArtistGenre);
            Assert.Equal(FreddyPohoda.StageId, dbFreddyPohoda.StageId);
            Assert.Equal(FreddyPohoda.StageName, dbFreddyPohoda.StageName);
        }
    }
}
