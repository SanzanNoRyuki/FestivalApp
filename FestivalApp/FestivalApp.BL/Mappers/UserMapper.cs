using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using System.Linq;

namespace FestivalApp.BL.Mappers
{
    public static class UserMapper
    {
        public static UserDetailModel UserToUserDetailModel(User user)
        {
            if (user == null) return null;
            return new UserDetailModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,

                AddressId = user.AddressId,

                Festivals = user.Festivals.Select(FestivalMapper.FestivalToFestivalListModel).ToList(),
                Tickets = user.Tickets.Select(TicketMapper.TicketToTicketListModel).ToList()
            };
        }

        public static User UserDetailModelToUser(UserDetailModel userDetailModel)
        {
            if (userDetailModel == null) return null;
            return new User
            {
                Id = userDetailModel.Id,
                Email = userDetailModel.Email,
                Name = userDetailModel.Name,
                PhoneNumber = userDetailModel.PhoneNumber,

                AddressId = userDetailModel.AddressId,

                Festivals = userDetailModel.Festivals.Select(FestivalMapper.FestivalListModelToFestival).ToList(),
                Tickets = userDetailModel.Tickets.Select(TicketMapper.TicketListModelToTicket).ToList(),
            };
        }

        public static UserListModel UserToUserListModel(User user)
        {
            if (user == null) return null;
            return new UserListModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }

        public static UserListModel UserDetailModelToUserListModel(UserDetailModel userDetailModel)
        {
            if (userDetailModel == null) return null;
            return new UserListModel
            {
                Id = userDetailModel.Id,
                Email = userDetailModel.Email,
                Name = userDetailModel.Name,
            };
        }

        public static User UserListModelToUser(UserListModel userListModel)
        {
            if (userListModel == null) return null;
            return new User
            {
                Id = userListModel.Id,
                Email = userListModel.Email,
                Name = userListModel.Name,
            };
        }

    }
}