using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using System;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class UserTests
    {
        public AreEqualMethod AreEqual = new AreEqualMethod();

        [Fact]
        public void Map_Basic_Values()
        {
            AreEqualMethod AreEqual = new AreEqualMethod();
            var addressEntity = new Address()
            {
                AddressLine1 = "addressLine",
                AddressLine2 = "hello",
                City = "city",
                Country = "country",
                PostalCode = "postalCode",
                State = "state"
            };

            var guidU = Guid.Parse("a3cffba7-055b-41f9-9bfc-0ce03511d115");
            var festivalEntity = new Festival()
            {
                Id = Guid.Parse("d767a5ac-7555-4baa-b9c7-17abd9f3fc70"),
                Title = "Festival Test",
                Start = DateTime.Parse("25 Feb 2021 15:00:00"),
                End = DateTime.Now,
                Capacity = 20,
                User = new User()
                {
                    Id = guidU,
                    Email = "something@something.com",
                    Name = "New TestUser",
                    AddressId = Guid.Parse("dd02e3bb-2a0d-484f-abf2-1a2c63da2e7b"),
                    Address = addressEntity,
                    PhoneNumber = "0026589654",
                },
                UserId = guidU
            };

            var userEntity = new User()
            {
                Id = guidU,
                Email = "something@something.com",
                Name = "New TestUser",
                AddressId = Guid.Parse("dd02e3bb-2a0d-484f-abf2-1a2c63da2e7b"),
                Address = addressEntity,
                PhoneNumber = "0026589654",
                Festivals =
                {
                    festivalEntity
                },
                Tickets =
                {
                    new Ticket()
                    {
                        Id = Guid.Parse("8a746f3b-5b7b-4ad0-9a33-ea5c88f8375f"),
                        Type = DAL.Entities.Enums.TicketType.Basic,
                        Length = DAL.Entities.Enums.TicketLength.FestivalWide,
                        PriceAmount = 50,
                        PriceCurrency = DAL.Entities.Enums.Currency.Euro,
                        StartDate = DateTime.Parse("25 Feb 2021 15:00:00"),
                        User = new User()
                        {
                            Id = guidU,
                            Email = "something@something.com",
                            Name = "New TestUser",
                            AddressId = Guid.Parse("dd02e3bb-2a0d-484f-abf2-1a2c63da2e7b"),
                            Address = addressEntity,
                            PhoneNumber = "0026589654",
                        },
                        UserId = guidU,
                        FestivalId = festivalEntity.Id,
                        Festival = festivalEntity
                    }
                }
            };

            var userDetail = UserMapper.UserToUserDetailModel(userEntity);
            var userList = UserMapper.UserToUserListModel(userEntity);
            var newUserEntity = UserMapper.UserListModelToUser(userList);
            var newUserList = UserMapper.UserDetailModelToUserListModel(userDetail);

            AreEqual.Equals(userEntity.Id, userDetail.Id, userList.Id, newUserEntity.Id, newUserList.Id);
            AreEqual.Equals(userEntity.Name, userDetail.Name, userList.Name, newUserEntity.Name, newUserList.Name);
            AreEqual.Equals(userEntity.Email, userDetail.Email, userList.Email, newUserEntity.Email, newUserList.Email);

            AreEqual.Equals(userEntity.PhoneNumber, userDetail.PhoneNumber);
            AreEqual.Equals(userEntity.AddressId, userDetail.AddressId);
        }
    }
}
