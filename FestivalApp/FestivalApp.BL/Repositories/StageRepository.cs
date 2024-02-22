using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Interfaces;
using FestivalApp.DAL;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.BL.Repositories
{
    public class StageRepository : IStage
    {
        // Setup
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public StageRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Base
        public StageDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Stages
                .Include(stage => stage.Festival)
                .Include(stage => stage.Shows)
                .SingleOrDefault(i => i.Id == id);

            return StageMapper.StageToStageDetailModel(entity);
        }

        public StageDetailModel CreateOrUpdate(StageDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newStage = StageMapper.StageDetailModelToStage(entity);
            newStage.Festival = dbContext.Festivals.Find(newStage.FestivalId);
            newStage.Shows = dbContext.Shows.Where(show => show.StageId == newStage.Id).ToList();

            dbContext.Stages.Update(newStage);
            dbContext.SaveChanges();

            return StageMapper.StageToStageDetailModel(newStage);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Stages.Find(id);
            if (entity == null) return;

            dbContext.Stages.Remove(entity);
            dbContext.SaveChanges();
        }

        // Extended
        public IEnumerable<StageListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            var stages = dbContext.Stages
                .Include(stage => stage.Festival);

            return stages.Select(stage => StageMapper.StageToStageListModel(stage)).ToList();
        }

        public IEnumerable<StageListModel> GetByFestival(Guid festivalId)
        {
            using var dbContext = _dbContextFactory.Create();

            var stages = dbContext.Stages
                .Include(stage => stage.Festival)
                .Where(stage => stage.Festival.Id == festivalId);

            return stages.Select(stage => StageMapper.StageToStageListModel(stage)).ToList();
        }

        public StageDetailModel GetByShow(Guid showId)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Stages
                .Include(stage => stage.Festival)
                .Include(stage => stage.Shows)
                .SingleOrDefault(stage => stage.Shows.Any(show => show.Id == showId));

            return StageMapper.StageToStageDetailModel(entity);
        }
    }
}
