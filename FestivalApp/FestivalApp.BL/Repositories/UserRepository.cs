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
    public class UserRepository : IUser
    {
        // Setup
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public UserRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Base
        public UserDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Users
                .Include(user => user.Address)
                .Include(user => user.Festivals)
                .ThenInclude(festival => festival.User)
                .Include(user => user.Tickets)
                .ThenInclude(ticket => ticket.Festival)
                .Include(user => user.Tickets)
                .ThenInclude(ticket => ticket.User)
                .SingleOrDefault(i => i.Id == id);

            return UserMapper.UserToUserDetailModel(entity);
        }

        public UserDetailModel CreateOrUpdate(UserDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newUser = UserMapper.UserDetailModelToUser(entity);
            newUser.Address = dbContext.Addresses.Find(newUser.AddressId);
            newUser.Festivals = dbContext.Festivals.Where(festival => festival.UserId == newUser.Id).ToList();
            newUser.Tickets = dbContext.Tickets.Where(ticket => ticket.UserId == newUser.Id)
                .Include(ticket => ticket.Festival)
                .ToList();

            dbContext.Users.Update(newUser);
            dbContext.SaveChanges();

            return UserMapper.UserToUserDetailModel(newUser);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Users.Find(id);
            if (entity == null) return;

            dbContext.Users.Remove(entity);
            dbContext.SaveChanges();
        }

        // Extended
        public IEnumerable<UserListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            var users = dbContext.Users;

            return users.Select(user => UserMapper.UserToUserListModel(user)).ToList();
        }

        public UserDetailModel GetByEmail(string email)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Users
                .Include(user => user.Address)
                .Include(user => user.Festivals)
                .Include(user => user.Tickets)
                .SingleOrDefault(user => user.Email == email);

            return UserMapper.UserToUserDetailModel(entity);
        }

        public UserDetailModel GetByFestival(Guid festivalId)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Users
                .Include(user => user.Address)
                .Include(user => user.Festivals)
                .Include(user => user.Tickets)
                .SingleOrDefault(user => user.Festivals.Any(festival => festival.Id == festivalId));

            return UserMapper.UserToUserDetailModel(entity);
        }

        public UserDetailModel GetByTicket(Guid ticketId)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Users
                .Include(user => user.Address)
                .Include(user => user.Festivals)
                .Include(user => user.Tickets)
                .ThenInclude(ticket => ticket.Festival)
                .ThenInclude(ticket => ticket.User)
                .SingleOrDefault(user => user.Tickets.Any(ticket => ticket.Id == ticketId));

            return UserMapper.UserToUserDetailModel(entity);
        }
    }
}
