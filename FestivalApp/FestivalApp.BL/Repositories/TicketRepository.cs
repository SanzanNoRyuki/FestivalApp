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
    public class TicketRepository : ITicket
    {
        // Setup
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public TicketRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Base
        public TicketDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Tickets
                .Include(ticket => ticket.Festival)
                .Include(ticket => ticket.User)
                .SingleOrDefault(i => i.Id == id);

            if (entity is { User: null }) return TicketMapper.TicketToTicketDetailModelNoUser(entity);
            return TicketMapper.TicketToTicketDetailModel(entity);
        }

        public TicketDetailModel CreateOrUpdate(TicketDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newTicket = TicketMapper.TicketDetailModelToTicket(entity);
            newTicket.Festival = dbContext.Festivals.Find(newTicket.FestivalId);
            newTicket.User = dbContext.Users.Find(newTicket.UserId);

            dbContext.Tickets.Update(newTicket);
            dbContext.SaveChanges();

            if (newTicket.User == null) return TicketMapper.TicketToTicketDetailModelNoUser(newTicket);
            return TicketMapper.TicketToTicketDetailModel(newTicket);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Tickets.Find(id);
            if (entity == null) return;

            dbContext.Tickets.Remove(entity);
            dbContext.SaveChanges();
        }

        // Extended
        public IEnumerable<TicketListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            var tickets = dbContext.Tickets
                .Include(ticket => ticket.Festival)
                .Include(ticket => ticket.User);

            return tickets.Select(ticket => ticket.User == null
                    ? TicketMapper.TicketToTicketListModelNoUser(ticket)
                    : TicketMapper.TicketToTicketListModel(ticket))
                .ToList();
        }

        public IEnumerable<TicketListModel> GetByUser(Guid userId)
        {
            using var dbContext = _dbContextFactory.Create();

            var tickets = dbContext.Tickets
                .Include(ticket => ticket.Festival)
                .Include(ticket => ticket.User)
                .Where(ticket => ticket.User.Id == userId);

            return tickets.Select(ticket => TicketMapper.TicketToTicketListModel(ticket)).ToList();
        }

        public IEnumerable<TicketListModel> GetByFestival(Guid festivalId)
        {
            using var dbContext = _dbContextFactory.Create();

            var tickets = dbContext.Tickets
                .Include(ticket => ticket.Festival)
                .Include(ticket => ticket.User)
                .Where(ticket => ticket.Festival.Id == festivalId);

            return tickets.Select(ticket => ticket.User == null
                    ? TicketMapper.TicketToTicketListModelNoUser(ticket)
                    : TicketMapper.TicketToTicketListModel(ticket))
                .ToList();
        }
    }
}
