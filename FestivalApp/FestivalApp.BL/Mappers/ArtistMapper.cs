using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using System.Linq;

namespace FestivalApp.BL.Mappers
{
    public class ArtistMapper
    {
        public static ArtistDetailModel ArtistToArtistDetailModel(Artist artist)
        {
            if (artist == null) return null;
            return new ArtistDetailModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Country = artist.Country,
                Genre = artist.Genre,
                ShortDescription = artist.ShortDescription,
                LongDescription = artist.LongDescription,
                PhotoPath = artist.PhotoPath,
                Shows = artist.Shows.Select(ShowMapper.ShowToShowListModelNoArtist).ToList()
            };
        }

        public static Artist ArtistDetailModelToArtist(ArtistDetailModel artistDetailModel)
        {
            if (artistDetailModel == null) return null;
            return new Artist
            {
                Id = artistDetailModel.Id,
                Name = artistDetailModel.Name,
                Country = artistDetailModel.Country,
                Genre = artistDetailModel.Genre,
                ShortDescription = artistDetailModel.ShortDescription,
                LongDescription = artistDetailModel.LongDescription,
                PhotoPath = artistDetailModel.PhotoPath,
                Shows = artistDetailModel.Shows.Select(ShowMapper.ShowListModelToShow).ToList(),
            };
        }

        public static ArtistListModel ArtistToArtistListModel(Artist artist)
        {
            if (artist == null) return null;
            return new ArtistListModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre
            };
        }

        public static ArtistListModel ArtistDetailModelToArtistListModel(ArtistDetailModel artistDetailModel)
        {
            if (artistDetailModel == null) return null;
            return new ArtistListModel
            {
                Id = artistDetailModel.Id,
                Name = artistDetailModel.Name,
                Genre = artistDetailModel.Genre
            };
        }

        public static Artist ArtistListModelToArtist(ArtistListModel artistListModel)
        {
            if (artistListModel == null) return null;
            return new Artist
            {
                Id = artistListModel.Id,
                Name = artistListModel.Name,
                Genre = artistListModel.Genre
            };
        }
    }
}