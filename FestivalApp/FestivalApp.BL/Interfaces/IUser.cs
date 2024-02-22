using FestivalApp.BL.Models;
using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Interfaces
{
    public interface IUser : IRepository<UserDetailModel>
    {
        IEnumerable<UserListModel> GetAll();
        UserDetailModel GetByEmail(string email);
        UserDetailModel GetByFestival(Guid festivalId);
        UserDetailModel GetByTicket(Guid ticketId);
    }
}

