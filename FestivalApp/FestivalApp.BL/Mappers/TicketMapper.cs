using System;
using FestivalApp.BL.Models;
using FestivalApp.BL.Models.Enums;
using FestivalApp.DAL.Entities;

namespace FestivalApp.BL.Mappers
{
    public class TicketMapper
    {
        public static TicketDetailModel TicketToTicketDetailModel(Ticket ticket)
        {
            if (ticket == null) return null;
            return new TicketDetailModel
            {
                Id = ticket.Id,
                Type = (TicketType)ticket.Type,
                Length = (TicketLength)ticket.Length,
                PriceAmount = ticket.PriceAmount,
                PriceCurrency = (Currency)ticket.PriceCurrency,
                StartDate = ticket.StartDate,

                UserId = ticket.UserId,
                UserEmail = ticket.User.Email,
                UserName = ticket.User.Name,

                FestivalId = ticket.FestivalId,
                FestivalTitle = ticket.Festival.Title,
                FestivalStart = ticket.Festival.Start,
                FestivalEnd = ticket.Festival.End
            };
        }

        public static Ticket TicketDetailModelToTicket(TicketDetailModel ticketDetailModel)
        {
            if (ticketDetailModel == null) return null;
            return new Ticket
            {
                Id = ticketDetailModel.Id,
                Type = (DAL.Entities.Enums.TicketType)ticketDetailModel.Type,
                Length = (DAL.Entities.Enums.TicketLength)ticketDetailModel.Length,
                PriceAmount = ticketDetailModel.PriceAmount,
                PriceCurrency = (DAL.Entities.Enums.Currency)ticketDetailModel.PriceCurrency,
                StartDate = ticketDetailModel.StartDate,

                UserId = ticketDetailModel.UserId,

                FestivalId = ticketDetailModel.FestivalId,
            };
        }

        // Used when showing new tickets without user
        public static TicketDetailModel TicketToTicketDetailModelNoUser(Ticket ticket)
        {
            if (ticket == null) return null;
            return new TicketDetailModel
            {
                Id = ticket.Id,
                Type = (TicketType)ticket.Type,
                Length = (TicketLength)ticket.Length,
                PriceAmount = ticket.PriceAmount,
                PriceCurrency = (Currency)ticket.PriceCurrency,
                StartDate = ticket.StartDate,

                FestivalId = ticket.FestivalId,
                FestivalTitle = ticket.Festival.Title,
                FestivalStart = ticket.Festival.Start,
                FestivalEnd = ticket.Festival.End
            };
        }

        public static TicketListModel TicketToTicketListModel(Ticket ticket)
        {
            if (ticket == null) return null;

            try
            {
                return new TicketListModel
                {
                    Id = ticket.Id,
                    Type = (TicketType)ticket.Type,
                    Length = (TicketLength)ticket.Length,
                    PriceAmount = ticket.PriceAmount,
                    PriceCurrency = (Currency)ticket.PriceCurrency,
                    StartDate = ticket.StartDate,

                    UserId = ticket.UserId,
                    UserEmail = ticket.User.Email,
                    UserName = ticket.User.Name,

                    FestivalId = ticket.FestivalId,
                    FestivalTitle = ticket.Festival.Title,
                    FestivalStart = ticket.Festival.Start,
                    FestivalEnd = ticket.Festival.End
                };
            }
            // Used when getting tickets for Festival
            catch (NullReferenceException)
            {
                return TicketToTicketListModelNoUser(ticket);
            }
        }

        public static TicketListModel TicketToTicketListModelNoUser(Ticket ticket)
        {
            if (ticket == null) return null;
            return new TicketListModel
            {
                Id = ticket.Id,
                Type = (TicketType)ticket.Type,
                Length = (TicketLength)ticket.Length,
                PriceAmount = ticket.PriceAmount,
                PriceCurrency = (Currency)ticket.PriceCurrency,
                StartDate = ticket.StartDate,

                FestivalId = ticket.FestivalId,
                FestivalTitle = ticket.Festival.Title,
                FestivalStart = ticket.Festival.Start,
                FestivalEnd = ticket.Festival.End
            };
        }

        public static TicketListModel TicketDetailModeToTicketListModel(TicketDetailModel ticketDetailModel)
        {
            if (ticketDetailModel == null) return null;
            return new TicketListModel
            {
                Id = ticketDetailModel.Id,
                Type = ticketDetailModel.Type,
                Length = ticketDetailModel.Length,
                PriceAmount = ticketDetailModel.PriceAmount,
                PriceCurrency = ticketDetailModel.PriceCurrency,
                StartDate = ticketDetailModel.StartDate,

                UserId = ticketDetailModel.UserId,
                UserEmail = ticketDetailModel.UserEmail,
                UserName = ticketDetailModel.UserName,

                FestivalId = ticketDetailModel.FestivalId,
                FestivalTitle = ticketDetailModel.FestivalTitle,
                FestivalStart = ticketDetailModel.FestivalStart,
                FestivalEnd = ticketDetailModel.FestivalEnd
            };
        }

        public static Ticket TicketListModelToTicket(TicketListModel ticketListModel)
        {
            if (ticketListModel == null) return null;
            return new Ticket
            {
                Id = ticketListModel.Id,
                Type = (DAL.Entities.Enums.TicketType)ticketListModel.Type,
                Length = (DAL.Entities.Enums.TicketLength)ticketListModel.Length,
                PriceAmount = ticketListModel.PriceAmount,
                PriceCurrency = (DAL.Entities.Enums.Currency)ticketListModel.PriceCurrency,
                StartDate = ticketListModel.StartDate,

                UserId = ticketListModel.UserId,

                FestivalId = ticketListModel.FestivalId,
            };
        }
    }
}