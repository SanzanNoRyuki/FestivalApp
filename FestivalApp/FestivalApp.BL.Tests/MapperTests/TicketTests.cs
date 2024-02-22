using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using System;
using FestivalApp.BL.Models.Enums;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class TicketTests
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

            var userEntity = new User()
            {
                Id = Guid.Parse("a3cffba7-055b-41f9-9bfc-0ce03511d115"),
                Email = "something@something.com",
                Name = "New TestUser",
                AddressId = Guid.Parse("dd02e3bb-2a0d-484f-abf2-1a2c63da2e7b"),
                Address = addressEntity,
                PhoneNumber = "0026589654"
            };

            var festivalEntity = new Festival()
            {
                Id = Guid.Parse("8f8f7350-2558-4548-8176-7d9d327c00f4"),
                Title = "Festival Test",
                Start = DateTime.Parse("25 Feb 2021 15:00:00"),
                End = DateTime.Now,
                Capacity = 20
            };

            var ticketEntity = new Ticket()
            {
                Id = Guid.Parse("af93c594-3eac-408f-bc9a-0268e4ad00c6"),
                Length = DAL.Entities.Enums.TicketLength.FestivalWide,
                Type = DAL.Entities.Enums.TicketType.Vip,
                PriceAmount = 1523,
                PriceCurrency = DAL.Entities.Enums.Currency.Czk,
                StartDate = DateTime.Parse("25 Feb 2021 15:00:00"),
                UserId = userEntity.Id,
                User = userEntity,
                FestivalId = festivalEntity.Id,
                Festival = festivalEntity
            };

            var ticketDetail = TicketMapper.TicketToTicketDetailModel(ticketEntity);
            var ticketList = TicketMapper.TicketToTicketListModel(ticketEntity);
            var newTicketEntity = TicketMapper.TicketListModelToTicket(ticketList);
            var newTicketList = TicketMapper.TicketDetailModeToTicketListModel(ticketDetail);

            AreEqual.Equals(ticketEntity.Id, ticketDetail.Id, ticketList.Id, newTicketEntity.Id, newTicketList.Id);
            AreEqual.Equals((TicketType)ticketEntity.Type, ticketDetail.Type, ticketList.Type, (TicketType)newTicketEntity.Type, newTicketList.Type);
            AreEqual.Equals((TicketLength)ticketEntity.Length, ticketDetail.Length, ticketList.Length, (TicketLength)newTicketEntity.Length, newTicketList.Length);
            AreEqual.Equals(ticketEntity.PriceAmount, ticketDetail.PriceAmount, ticketList.PriceAmount, newTicketEntity.PriceAmount, newTicketList.PriceAmount);
            AreEqual.Equals((Currency)ticketEntity.PriceCurrency, ticketDetail.PriceCurrency, ticketList.PriceCurrency, (Currency)newTicketEntity.PriceCurrency, newTicketList.PriceCurrency);
            AreEqual.Equals(ticketEntity.StartDate, ticketDetail.StartDate, ticketList.StartDate, newTicketEntity.StartDate, newTicketList.StartDate);
            AreEqual.Equals(ticketEntity.UserId, ticketDetail.UserId, ticketList.UserId, newTicketEntity.UserId, newTicketList.UserId);
            AreEqual.Equals(ticketEntity.FestivalId, ticketDetail.FestivalId, ticketList.FestivalId, newTicketEntity.FestivalId, newTicketList.FestivalId);

            AreEqual.Equals(ticketEntity.Festival.Start, ticketDetail.FestivalStart, ticketList.FestivalStart, newTicketList.FestivalStart);

            AreEqual.Equals(ticketEntity.User.Name, ticketDetail.UserName, ticketList.UserName, newTicketList.UserName);
            AreEqual.Equals(ticketEntity.User.Email, ticketDetail.UserEmail, ticketList.UserEmail, newTicketList.UserEmail);
        }
    }
}
