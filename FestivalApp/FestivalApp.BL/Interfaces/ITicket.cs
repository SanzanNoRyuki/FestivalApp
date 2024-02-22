using System;
using System.Collections.Generic;
using FestivalApp.BL.Models;

namespace FestivalApp.BL.Interfaces
{
    public interface ITicket : IRepository<TicketDetailModel>
    {
        IEnumerable<TicketListModel> GetAll();
        IEnumerable<TicketListModel> GetByUser(Guid userId);
        IEnumerable<TicketListModel> GetByFestival(Guid festivalId);
    }
}