using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using System.Linq;

namespace FestivalApp.BL.Mappers
{
    public class FestivalMapper
    {
        public static FestivalDetailModel FestivalToFestivalDetailModel(Festival festival)
        {
            if (festival == null) return null;
            return new FestivalDetailModel
            {
                Id = festival.Id,
                Title = festival.Title,
                Start = festival.Start,
                End = festival.End,
                Capacity = festival.Capacity,

                UserId = festival.UserId,
                UserEmail = festival.User.Email,
                UserName = festival.User.Name,
                UserAddressId = festival.User.AddressId,
                UserPhoneNumber = festival.User.PhoneNumber,

                Stages = festival.Stages.Select(StageMapper.StageToStageListModel).ToList(),
                Tickets = festival.Tickets.Select(TicketMapper.TicketToTicketListModel).ToList(),
            };
        }

        public static Festival FestivalDetailModelToFestival(FestivalDetailModel festivalDetailModel)
        {
            if (festivalDetailModel == null) return null;
            return new Festival
            {
                Id = festivalDetailModel.Id,
                Title = festivalDetailModel.Title,
                Start = festivalDetailModel.Start,
                End = festivalDetailModel.End,
                Capacity = festivalDetailModel.Capacity,

                UserId = festivalDetailModel.UserId,

                Stages = festivalDetailModel.Stages.Select(StageMapper.StageListModelToStage).ToList(),
                Tickets = festivalDetailModel.Tickets.Select(TicketMapper.TicketListModelToTicket).ToList(),
            };
        }

        public static FestivalListModel FestivalToFestivalListModel(Festival festival)
        {
            if (festival == null) return null;
            return new FestivalListModel
            {
                Id = festival.Id,
                Title = festival.Title,
                Start = festival.Start,
                End = festival.End,

                UserId = festival.UserId,
                UserEmail = festival.User.Email,
                UserName = festival.User.Name,

            };
        }

        public static FestivalListModel FestivalDetailModelToFestivalListModel(FestivalDetailModel festivalDetailModel)
        {
            if (festivalDetailModel == null) return null;
            return new FestivalListModel
            {
                Id = festivalDetailModel.Id,
                Title = festivalDetailModel.Title,
                Start = festivalDetailModel.Start,
                End = festivalDetailModel.End,

                UserId = festivalDetailModel.UserId,
                UserEmail = festivalDetailModel.UserEmail,
                UserName = festivalDetailModel.UserName,
            };
        }

        public static Festival FestivalListModelToFestival(FestivalListModel festivalListModel)
        {
            if (festivalListModel == null) return null;
            return new Festival
            {
                Id = festivalListModel.Id,
                Title = festivalListModel.Title,
                Start = festivalListModel.Start,
                End = festivalListModel.End,
                UserId = festivalListModel.UserId,
            };
        }
    }
}