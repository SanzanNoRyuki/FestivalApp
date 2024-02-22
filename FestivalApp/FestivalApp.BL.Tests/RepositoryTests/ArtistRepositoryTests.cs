using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoryTests
{
    public sealed class ArtistRepositoryTests : IDisposable
    {
        // Setup
        private readonly ArtistRepository _artistRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public ArtistRepositoryTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(ArtistRepositoryTests));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _artistRepositorySUT = new ArtistRepository(_dbContextFactory);
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
            Assert.Null(dbx.Artists.Find(id));

            var dbModel = _artistRepositorySUT.GetById(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetById_WithExistingItem()
        {
            var seedModel = ArtistMapper.ArtistToArtistDetailModel(ArtistSeeds.FreddyMercury);

            var dbModel = _artistRepositorySUT.GetById(seedModel.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.Country, dbModel.Country);
            Assert.Equal(seedModel.Genre, dbModel.Genre);
            Assert.Equal(seedModel.ShortDescription, dbModel.ShortDescription);
            Assert.Equal(seedModel.LongDescription, dbModel.LongDescription);
            Assert.Equal(seedModel.PhotoPath, dbModel.PhotoPath);
        }

        [Fact]
        public void CreateOrUpdate_WithNonExistingItem()
        {
            var newModel = new ArtistDetailModel
            {
                Name = "Rolling Stones",
                Country = "United Kingdom",
                Genre = "Rock and roll",
                ShortDescription = "English rock band formed in London",
                LongDescription = "Rooted in blues and early rock and roll, " +
                                  "the Rolling Stones started out playing " +
                                  "covers and were at the forefront of the " +
                                  "British Invasion in 1964, also being " +
                                  "identified with the youthful and " +
                                  "rebellious counterculture of the 1960s. " +
                                  "They then found greater success with " +
                                  "their own material as '(I Can't Get No)" +
                                  " Satisfaction', 'Get Off of My Cloud' and " +
                                  "'Paint It Black' became No. 1 hits in the" +
                                  " UK, North America, Australia and Europe." +
                                  " Aftermath (1966) – their first entirely" +
                                  " original album – is considered the most" +
                                  " important of their formative records." +
                                  " In 1967, they had the double-sided hit" +
                                  " 'Ruby Tuesday'/'Let's Spend the Night" +
                                  " Together' and then experimented with" +
                                  " psychedelic rock on Their Satanic" +
                                  " Majesties Request. They went back" +
                                  " to their roots with such hits as" +
                                  " 'Jumpin' Jack Flash' (1968) and " +
                                  "'Honky Tonk Women' (1969), and albums" +
                                  " such as Beggars Banquet (1968)," +
                                  " featuring 'Sympathy for the Devil'," +
                                  " and Let It Bleed (1969), featuring" +
                                  " 'You Can't Always Get What You Want'" +
                                  " and 'Gimme Shelter'. Let It Bleed was" +
                                  " the first of five straight No. 1 albums" +
                                  " in the UK. In 1969, they were first" +
                                  " introduced on stage as 'The Greatest" +
                                  " Rock and Roll Band in the World'.",
                PhotoPath = @"img\artist.png",
            };

            var dbModel = _artistRepositorySUT.CreateOrUpdate(newModel);

            Assert.NotNull(dbModel);
            Assert.Equal(newModel.Name, dbModel.Name);
            Assert.Equal(newModel.Country, dbModel.Country);
            Assert.Equal(newModel.Genre, dbModel.Genre);
            Assert.Equal(newModel.ShortDescription, dbModel.ShortDescription);
            Assert.Equal(newModel.LongDescription, dbModel.LongDescription);
            Assert.Equal(newModel.PhotoPath, dbModel.PhotoPath);
        }

        [Fact]
        public void CreateOrUpdate_WithExistingItem()
        {
            var updatedModel = ArtistMapper.ArtistToArtistDetailModel(ArtistSeeds.Queen);
            updatedModel.Name = "Rolling Stones";
            updatedModel.Country = "United Kingdom";
            updatedModel.Genre = "Rock";                                              // Same value as seed
            // updatedModel.ShortDescription = "English rock band formed in London";  // Unchanged seed value
            updatedModel.LongDescription = "Rooted in blues and early rock and roll, " +
                                           "the Rolling Stones started out playing " +
                                           "covers and were at the forefront of the " +
                                           "British Invasion in 1964, also being " +
                                           "identified with the youthful and " +
                                           "rebellious counterculture of the 1960s. " +
                                           "They then found greater success with " +
                                           "their own material as '(I Can't Get No)" +
                                           " Satisfaction', 'Get Off of My Cloud' and " +
                                           "'Paint It Black' became No. 1 hits in the" +
                                           " UK, North America, Australia and Europe." +
                                           " Aftermath (1966) – their first entirely" +
                                           " original album – is considered the most" +
                                           " important of their formative records." +
                                           " In 1967, they had the double-sided hit" +
                                           " 'Ruby Tuesday'/'Let's Spend the Night" +
                                           " Together' and then experimented with" +
                                           " psychedelic rock on Their Satanic" +
                                           " Majesties Request. They went back" +
                                           " to their roots with such hits as" +
                                           " 'Jumpin' Jack Flash' (1968) and " +
                                           "'Honky Tonk Women' (1969), and albums" +
                                           " such as Beggars Banquet (1968)," +
                                           " featuring 'Sympathy for the Devil'," +
                                           " and Let It Bleed (1969), featuring" +
                                           " 'You Can't Always Get What You Want'" +
                                           " and 'Gimme Shelter'. Let It Bleed was" +
                                           " the first of five straight No. 1 albums" +
                                           " in the UK. In 1969, they were first" +
                                           " introduced on stage as 'The Greatest" +
                                           " Rock and Roll Band in the World'.";
            updatedModel.PhotoPath = @"img\artist.png";

            var dbModel = _artistRepositorySUT.CreateOrUpdate(updatedModel);

            Assert.NotNull(dbModel);
            Assert.Equal(updatedModel.Name, dbModel.Name);
            Assert.Equal(updatedModel.Country, dbModel.Country);
            Assert.Equal(updatedModel.Genre, dbModel.Genre);
            Assert.Equal(updatedModel.ShortDescription, dbModel.ShortDescription);
            Assert.Equal(updatedModel.LongDescription, dbModel.LongDescription);
            Assert.Equal(updatedModel.PhotoPath, dbModel.PhotoPath);
        }

        [Fact]
        public void Delete_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Artists.Find(id));

            _artistRepositorySUT.Delete(id);

            Assert.Null(dbx.Artists.Find(id));
        }

        [Fact]
        public void Delete_WithExistingItem()
        {
            Guid id = ArtistSeeds.FreddyMercury.Id;

            _artistRepositorySUT.Delete(id);

            using var dbx = _dbContextFactory.Create();
            Assert.Null(dbx.Artists.Find(id));
        }

        // Extended tests
        [Fact]
        public void GetAll()
        {
            var FreddyMercury = ArtistMapper.ArtistToArtistListModel(ArtistSeeds.FreddyMercury);
            var Queen = ArtistMapper.ArtistToArtistListModel(ArtistSeeds.Queen);

            var dbList = _artistRepositorySUT.GetAll();

            var dbFreddyMercury = dbList.Single(artist => artist.Id == FreddyMercury.Id);
            var dbQueen = dbList.Single(artist => artist.Id == Queen.Id);
            Assert.Equal(FreddyMercury.Name, dbFreddyMercury.Name);
            Assert.Equal(FreddyMercury.Genre, dbFreddyMercury.Genre);
            Assert.Equal(Queen.Name, dbQueen.Name);
            Assert.Equal(Queen.Genre, dbQueen.Genre);
        }

        [Fact]
        public void GetByShow_WithNonExistingItem()
        {
            using var dbx = _dbContextFactory.Create();
            Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.Null(dbx.Shows.Find(id));

            var dbModel = _artistRepositorySUT.GetByShow(id);

            Assert.Null(dbModel);
        }

        [Fact]
        public void GetByShow_WithExistingItem()
        {
            var seedModel = ArtistMapper.ArtistToArtistDetailModel(ArtistSeeds.FreddyMercury);

            var dbModel = _artistRepositorySUT.GetByShow(ShowSeeds.FreddyRFP.Id);

            Assert.NotNull(dbModel);
            Assert.Equal(seedModel.Name, dbModel.Name);
            Assert.Equal(seedModel.Country, dbModel.Country);
            Assert.Equal(seedModel.Genre, dbModel.Genre);
            Assert.Equal(seedModel.ShortDescription, dbModel.ShortDescription);
            Assert.Equal(seedModel.LongDescription, dbModel.LongDescription);
            Assert.Equal(seedModel.PhotoPath, dbModel.PhotoPath);
        }
    }
}
