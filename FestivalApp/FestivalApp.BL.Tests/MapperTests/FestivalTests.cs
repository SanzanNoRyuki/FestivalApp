using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using System;
using System.Linq;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class FestivalTests
    {
        public AreEqualMethod AreEqual = new AreEqualMethod();

        [Fact]
        public void Map_Basic_Values()
        {
            AreEqualMethod AreEqual = new AreEqualMethod();
            // Arrange
            var guidA = Guid.Parse("dd02e3bb-2a0d-484f-abf2-1a2c63da2e7b");
            var addressEntity = new Address()
            {
                Id = guidA,
                AddressLine1 = "addressLine",
                AddressLine2 = "hello",
                City = "city",
                Country = "country",
                PostalCode = "postalCode",
                State = "state"
            };

            var userEntity = new User()
            {
                Id = Guid.Parse("a3cffba7-055b-41f9-9bfc-0ce03511d115"),
                Email = "something@something.com",
                Name = "New TestUser",
                AddressId = guidA,
                Address = addressEntity,
                PhoneNumber = "0026589654"
            };

            var guidF = Guid.Parse("8f8f7350-2558-4548-8176-7d9d327c00f4");
            var guidT = Guid.Parse("3fae5da5-dfe4-4a48-af34-23bdf35195d6");
            var ticketEntity = new Ticket()
            {
                Id = guidT,
                Type = DAL.Entities.Enums.TicketType.Basic,
                Length = DAL.Entities.Enums.TicketLength.FestivalWide,
                PriceAmount = 50,
                PriceCurrency = DAL.Entities.Enums.Currency.Euro,
                StartDate = DateTime.Parse("25 Feb 2021 15:00:00"),
                UserId = userEntity.Id,
                User = userEntity,
                FestivalId = guidF,
                Festival = new Festival()
                {
                    Id = guidF,
                    Title = "Festival Test",
                    Start = DateTime.Parse("25 Feb 2021 15:00:00"),
                    End = DateTime.Now,
                    Capacity = 20,
                    UserId = userEntity.Id,
                    User = userEntity
                }
            };

            var guidS = Guid.Parse("fad59428-0755-43c1-990e-81ef98e69996");
            var stageEntity = new Stage()
            {
                Id = guidS,
                Name = "Stage test",
                FestivalId = guidF,
                Festival = new Festival()
                {
                    Id = guidF,
                    Title = "Festival Test",
                    Start = DateTime.Parse("25 Feb 2021 15:00:00"),
                    End = DateTime.Now,
                    Capacity = 20,
                    UserId = userEntity.Id,
                    User = userEntity
                }
            };
            var festivalEntity = new Festival()
            {
                Id = guidF,
                Title = "Festival Test",
                Start = DateTime.Parse("25 Feb 2021 15:00:00"),
                End = DateTime.Now,
                Capacity = 20,
                UserId = userEntity.Id,
                User = userEntity,
                Stages = { stageEntity },
                Tickets = { ticketEntity }
            };

            // Act + Assert
            var festivalDetail = FestivalMapper.FestivalToFestivalDetailModel(festivalEntity);
            var festivalList = FestivalMapper.FestivalToFestivalListModel(festivalEntity);

            Assert.Equal(festivalEntity.Stages.ElementAt(0).Id, festivalDetail.Stages.ElementAt(0).Id);
            Assert.Equal(festivalEntity.Tickets.ElementAt(0).Id, festivalDetail.Tickets.ElementAt(0).Id);

            AreEqual.Equals(festivalEntity.Id, festivalDetail.Id, festivalList.Id);
            AreEqual.Equals(festivalEntity.Title, festivalDetail.Title, festivalList.Title);
            AreEqual.Equals(festivalEntity.Start, festivalDetail.Start, festivalList.Start);
            AreEqual.Equals(festivalEntity.End, festivalDetail.End, festivalList.End);
            AreEqual.Equals(festivalEntity.UserId, festivalDetail.UserId, festivalList.UserId);
            AreEqual.Equals(festivalEntity.User.Email, festivalDetail.UserEmail, festivalList.UserEmail);
            AreEqual.Equals(festivalEntity.User.Name, festivalDetail.UserName, festivalList.UserName);

            var newFestivalEntity = FestivalMapper.FestivalListModelToFestival(festivalList);
            AreEqual.Equals(newFestivalEntity.Id, festivalList.Id, festivalEntity.Id);
        }
    }
}
