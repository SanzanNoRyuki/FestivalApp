using Xunit;
using System;
using System.Linq;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Tests
{
    public class DbContextTests : IDisposable
    {
        private readonly FestivalDbContext _dbContextRight;
        private readonly FestivalDbContext _dbContextLeft;

        public DbContextTests()
        {
            var dbContextFactory = new DbContextFactory(nameof(DbContextTests));
            _dbContextRight = dbContextFactory.Create();
            _dbContextLeft = dbContextFactory.Create();
        }

        [Theory]
        [InlineData("lol", 250)]
        [InlineData("another name", 0)]
        [InlineData("another_another name", uint.MaxValue)]
        public void Add_Festival_Entity(string title, uint capacity)
        {
            var festivalEntity = new Festival()
            {
                Title = title,
                Start = DateTime.Today,
                End = DateTime.Now,
                Capacity = capacity,
                User = new User()
                {
                    Address = new Address()
                    {
                        PostalCode = "12345"
                    },
                    Email = "hahah@post.cz",
                    Name = "Jiřina Bohadalová",
                    PhoneNumber = "25142625141459362517896"
                }
            };

            _dbContextRight.Festivals.Add(festivalEntity);
            _dbContextRight.SaveChanges();

            var festivalDb = _dbContextLeft.Festivals
                .Include(festivalUser => festivalUser.User)
                .ThenInclude(userAddress => userAddress.Address)
                .FirstOrDefault(i => i.Id == festivalEntity.Id);
            Assert.Equal(festivalEntity, festivalDb);
        }

        [Theory]
        [InlineData("Obama", "USA", "Rock", "null", "Originally president, now proud rock singer.", "/dev/null")]
        [InlineData(null, null, null, null, null, null)]
        public void Add_Artist_Entity(
            string name,
            string country,
            string genre,
            string shortDescription,
            string longDescription,
            string photoPath
        )
        {
            var artistEntity = new Artist()
            {
                Name = name,
                Country = country,
                Genre = genre,
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                PhotoPath = photoPath
            };

            _dbContextRight.Artists.Add(artistEntity);
            _dbContextRight.SaveChanges();

            var interpretDb = _dbContextLeft.Artists.FirstOrDefault(i => i.Id == artistEntity.Id);
            Assert.Equal(artistEntity, interpretDb);
        }

        [Fact]
        public void Add_Stage_Entity()
        {
            var stageEntity = new Stage()
            {
                Description = "South-West stage",
                Name = "Stage01",
                PhotoPath = "/dev/null"
            };

            _dbContextRight.Stages.Add(stageEntity);
            _dbContextRight.SaveChanges();

            var stageDb = _dbContextLeft.Stages.FirstOrDefault(i => i.Id == stageEntity.Id);
            Assert.Equal(stageEntity, stageDb);
        }

        [Fact]
        public void Add_User_Entity()
        {
            var userEntity = new User()
            {
                Name = "Jan Novotný",
                Email = "lol@post123.xyz",
                Address = new Address()
                {
                    AddressLine1 = "abcd 26",
                    AddressLine2 = null,
                    City = "Brno",
                    Country = "Jihomoravský",
                    PostalCode = "420 69",
                    State = "Czechia"
                },
                PhoneNumber = "123 456 789",
                Tickets =
                {
                    new Ticket()
                    {
                        Length = TicketLength.OneDay,
                        Type = TicketType.Vip,
                        PriceAmount = 55.5M,
                        PriceCurrency = Currency.Euro
                    }
                }
            };

            _dbContextRight.Users.Add(userEntity);
            _dbContextRight.SaveChanges();

            var userDb = _dbContextLeft.Users
                .Include(user => user.Address)
                .Include(user => user.Tickets)
                .FirstOrDefault(i => i.Id == userEntity.Id);
            Assert.Equal(userEntity, userDb);
        }

        [Theory]
        [InlineData(TicketLength.FestivalWide, TicketType.Basic, Currency.Czk)]
        [InlineData(TicketLength.OneDay, TicketType.Vip, Currency.Euro)]
        [InlineData(TicketLength.OneDay, TicketType.Basic, Currency.Usd)]
        public void Add_Ticket_Entity(
            TicketLength ticketLength, TicketType ticketType, Currency currency
        )
        {
            var ticketEntity = new Ticket()
            {
                Length = ticketLength,
                Type = ticketType,
                PriceAmount = 55.5M,
                PriceCurrency = currency,
                StartDate = DateTime.Now.AddDays(5)
            };

            _dbContextRight.Tickets.Add(ticketEntity);
            _dbContextRight.SaveChanges();

            var ticketDb = _dbContextLeft.Tickets.FirstOrDefault(i => i.Id == ticketEntity.Id);
            Assert.Equal(ticketEntity, ticketDb);

            var failTicketEntity = new Ticket()
            {
                Length = TicketLength.OneDay,
                Type = TicketType.Vip,
                PriceAmount = 4200M,
                PriceCurrency = Currency.Czk
            };

            Assert.NotEqual(failTicketEntity, ticketDb);
        }

        [Fact]
        public void Add_Shows()
        {
            var show = new Show()
            {
                Artist = new Artist()
                {
                    Name = "Lubomír Volný",
                    Country = "Czechia",
                    Genre = "Reggae"
                },
                StartPlaying = DateTime.Now,
                EndPlaying = DateTime.Now.AddMinutes(50),
                LengthOfPlaying = TimeSpan.FromMinutes(50),

                Stage = new Stage()
                {
                    Description = "aaaa",
                    Name = "bbbb"
                }
            };

            _dbContextRight.Shows.Add(show);
            _dbContextRight.SaveChanges();

            var showDb = _dbContextLeft.Shows
                .Include(artistStage => artistStage.Artist)
                .Include(artistStage => artistStage.Stage)
                .FirstOrDefault(i => i.Id == show.Id);
            Assert.Equal(show, showDb);

        }
        public void Dispose()
        {
            _dbContextRight.Dispose();
            _dbContextLeft.Dispose();
        }
    }
}