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
    public class FestivalRepository : IFestival
    {
        // Setup
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public FestivalRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Base
        public FestivalDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Festivals
                .Include(festival => festival.User)
                .Include(festival => festival.Tickets)
                .ThenInclude(ticket => ticket.User)
                .Include(festival => festival.Stages)
                .SingleOrDefault(i => i.Id == id);

            return FestivalMapper.FestivalToFestivalDetailModel(entity);
        }

        public FestivalDetailModel CreateOrUpdate(FestivalDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newFestival = FestivalMapper.FestivalDetailModelToFestival(entity);
            newFestival.User = dbContext.Users.Find(newFestival.UserId);
            newFestival.Tickets = dbContext.Tickets.Where(ticket => ticket.FestivalId == newFestival.Id).ToList();
            newFestival.Stages = dbContext.Stages.Where(stage => stage.FestivalId == newFestival.Id).ToList();

            dbContext.Festivals.Update(newFestival);
            dbContext.SaveChanges();

            return FestivalMapper.FestivalToFestivalDetailModel(newFestival);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Festivals.Find(id);
            if (entity == null) return;

            dbContext.Festivals.Remove(entity);
            dbContext.SaveChanges();
        }

        // Extended
        public IEnumerable<FestivalListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            var festivals = dbContext.Festivals
                .Include(festival => festival.User);

            return festivals.Select(festival => FestivalMapper.FestivalToFestivalListModel(festival)).ToList();
        }

        public IEnumerable<FestivalListModel> GetByOwner(Guid userId)
        {
            using var dbContext = _dbContextFactory.Create();

            var festivals = dbContext.Festivals
                .Include(festival => festival.User)
                .Where(festival => festival.User.Id == userId);

            return festivals.Select(festival => FestivalMapper.FestivalToFestivalListModel(festival)).ToList();
        }

        public FestivalDetailModel GetByStage(Guid stageId)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Festivals
                .Include(festival => festival.User)
                .Include(festival => festival.Stages)
                .Include(festival => festival.Tickets)
                .ThenInclude(ticket => ticket.User)
                .SingleOrDefault(festival => festival.Stages.Any(stage => stage.Id == stageId));

            return FestivalMapper.FestivalToFestivalDetailModel(entity);
        }

        public FestivalDetailModel GetByTicket(Guid ticketId)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Festivals
                .Include(festival => festival.User)
                .Include(festival => festival.Stages)
                .Include(festival => festival.Tickets)
                .ThenInclude(ticket => ticket.User)
                .SingleOrDefault(festival => festival.Tickets.Any(ticket => ticket.Id == ticketId));

            return FestivalMapper.FestivalToFestivalDetailModel(entity);
        }
    }
}
