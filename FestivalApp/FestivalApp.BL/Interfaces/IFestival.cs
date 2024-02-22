using FestivalApp.BL.Models;
using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Interfaces
{
    public interface IFestival : IRepository<FestivalDetailModel>
    {
        IEnumerable<FestivalListModel> GetAll();
        IEnumerable<FestivalListModel> GetByOwner(Guid userId);
        FestivalDetailModel GetByStage(Guid stageId);
        FestivalDetailModel GetByTicket(Guid ticketId);
    }
}