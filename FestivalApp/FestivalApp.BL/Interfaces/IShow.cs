using FestivalApp.BL.Models;
using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Interfaces
{
    public interface IShow : IRepository<ShowDetailModel>
    {
        IEnumerable<ShowListModel> GetByStage(Guid stageId);
        IEnumerable<ShowListModel> GetByArtist(Guid artistId);
    }
}