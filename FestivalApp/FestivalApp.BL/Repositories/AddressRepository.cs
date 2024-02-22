using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Interfaces;
using FestivalApp.DAL;
using FestivalApp.DAL.Factories;

namespace FestivalApp.BL.Repositories
{
    public class AddressRepository : IAddress
    {
        // Setup
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public AddressRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory ?? throw new ArgumentNullException();
        }

        // Base
        public AddressDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Addresses.Find(id);

            return AddressMapper.AddressToAddressDetailModel(entity);
        }

        public AddressDetailModel CreateOrUpdate(AddressDetailModel entity)
        {
            using var dbContext = _dbContextFactory.Create();

            var newAddress = AddressMapper.AddressDetailModelToAddress(entity);
            newAddress.User = dbContext.Users.Find(newAddress.UserId);

            dbContext.Addresses.Update(newAddress);
            dbContext.SaveChanges();

            return AddressMapper.AddressToAddressDetailModel(newAddress);
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Addresses.Find(id);
            if (entity == null) return;

            dbContext.Addresses.Remove(entity);
            dbContext.SaveChanges();
        }

        // Extended
        public AddressDetailModel GetByUser(Guid userId)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Addresses.SingleOrDefault(address => address.UserId == userId);

            return AddressMapper.AddressToAddressDetailModel(entity);
        }
    }
}
