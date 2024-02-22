using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Interfaces;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.BL.Repositories
{
    public class ArtistRepository : IArtist
    {
        // dbContextFactory
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public ArtistRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Base Repository
        public ArtistDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var artistEntity = dbContext.Artists
                .Include(artist => artist.Shows)
                .ThenInclude(show => show.Stage)
                .SingleOrDefault(i => i.Id == id);

            return ArtistMapper.ArtistToArtistDetailModel(artistEntity);
        }

        public ArtistDetailModel CreateOrUpdate(ArtistDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newArtist = ArtistMapper.ArtistDetailModelToArtist(entity);
            newArtist.Shows = dbContext.Shows.Where(show => show.ArtistId == newArtist.Id)
                .Include(show => show.Stage)
                .ToList();

            dbContext.Artists.Update(newArtist);
            dbContext.SaveChanges();

            return ArtistMapper.ArtistToArtistDetailModel(newArtist);
        }

        // Unique
        public IEnumerable<ArtistListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            return dbContext.Artists
                .Select(artist => ArtistMapper.ArtistToArtistListModel(artist))
                .ToList();
        }

        public ArtistDetailModel GetByShow(Guid showId)
        {
            using var dbContext = _dbContextFactory.Create();

            var artistEntity = dbContext.Artists
                .Include(artist => artist.Shows)
                .ThenInclude(show => show.Stage)
                .SingleOrDefault(artist => artist.Shows.Any(show => show.Id == showId));

            return ArtistMapper.ArtistToArtistDetailModel(artistEntity);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var artistEntity = dbContext.Artists.Find(id);
            if (artistEntity == null) return;

            dbContext.Artists.Remove(artistEntity);
            dbContext.SaveChanges();
        }
    }
}
