using FestivalApp.BL.Models;
using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Interfaces
{
    public interface IArtist : IRepository<ArtistDetailModel>
    {
        IEnumerable<ArtistListModel> GetAll();
        ArtistDetailModel GetByShow(Guid showId);
    }
}