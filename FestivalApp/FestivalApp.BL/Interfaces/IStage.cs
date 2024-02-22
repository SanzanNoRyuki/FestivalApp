using FestivalApp.BL.Models;
using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Interfaces
{
    public interface IStage : IRepository<StageDetailModel>
    {
        IEnumerable<StageListModel> GetAll();
        IEnumerable<StageListModel> GetByFestival(Guid festivalId);
        StageDetailModel GetByShow(Guid showId);
    }
}