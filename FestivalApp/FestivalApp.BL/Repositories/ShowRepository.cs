using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Interfaces;
using FestivalApp.DAL;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.BL.Repositories
{
    public class ShowRepository : IShow
    {
        // Setup
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public ShowRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Supporting methods
        private bool checkOverlap(Show show1, Show show2)
        {
            // Update is not overlap
            if (show1.Id == show2.Id) return false;

            if (show1.StartPlaying < show2.StartPlaying) return show1.EndPlaying > show2.StartPlaying;
            else return show2.EndPlaying < show1.StartPlaying;
        }

        // Base
        public ShowDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Shows
                .Include(show => show.Artist)
                .Include(show => show.Stage)
                .SingleOrDefault(i => i.Id == id);

            return ShowMapper.ShowToShowDetailModel(entity);
        }

        public ShowDetailModel CreateOrUpdate(ShowDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newShow = ShowMapper.ShowDetailModelToShow(entity);
            newShow.Artist = dbContext.Artists.Find(newShow.ArtistId);
            newShow.Stage = dbContext.Stages.Find(newShow.StageId);

            // Time slot overlap
            var relatedShows = dbContext.Shows
                .Where(show => show.StageId == newShow.StageId || show.ArtistId == newShow.ArtistId)
                .AsNoTracking()
                .ToList();
            if (relatedShows.Any(relatedShow => checkOverlap(relatedShow, newShow))) return null;

            dbContext.Shows.Update(newShow);
            dbContext.SaveChanges();

            return ShowMapper.ShowToShowDetailModel(newShow);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Shows.Find(id);
            if (entity == null) return;

            dbContext.Shows.Remove(entity);
            dbContext.SaveChanges();
        }

        // Extended
        public IEnumerable<ShowListModel> GetByStage(Guid stageId)
        {
            using var dbContext = _dbContextFactory.Create();

            var shows = dbContext.Shows
                .Include(show => show.Artist)
                .Include(show => show.Stage)
                .Where(show => show.Stage.Id == stageId);

            return shows.Select(show => ShowMapper.ShowToShowListModel(show)).ToList();
        }

        public IEnumerable<ShowListModel> GetByArtist(Guid artistId)
        {
            using var dbContext = _dbContextFactory.Create();

            var shows = dbContext.Shows
                .Include(show => show.Artist)
                .Include(show => show.Stage)
                .Where(show => show.Artist.Id == artistId);

            return shows.Select(show => ShowMapper.ShowToShowListModel(show)).ToList();
        }
    }
}
