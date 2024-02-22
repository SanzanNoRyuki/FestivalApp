using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
namespace FestivalApp.BL.Mappers
{
    public class ShowMapper
    {
        public static ShowDetailModel ShowToShowDetailModel(Show show)
        {
            if (show == null) return null;
            return new ShowDetailModel
            {
                Id = show.Id,
                StartPlaying = show.StartPlaying,
                EndPlaying = show.EndPlaying,
                LengthOfPlaying = show.LengthOfPlaying,

                ArtistId = show.ArtistId,
                ArtistName = show.Artist.Name,
                ArtistCountry = show.Artist.Country,
                ArtistGenre = show.Artist.Genre,
                ArtistShortDescription = show.Artist.ShortDescription,
                ArtistLongDescription = show.Artist.LongDescription,
                ArtistPhotoPath = show.Artist.PhotoPath,

                StageId = show.StageId,
                StageName = show.Stage.Name,
                StageDescription = show.Stage.Description,
                StagePhotoPath = show.Stage.PhotoPath
            };
        }

        public static Show ShowDetailModelToShow(ShowDetailModel showDetailModel)
        {
            if (showDetailModel == null) return null;
            return new Show
            {
                Id = showDetailModel.Id,
                ArtistId = showDetailModel.ArtistId,
                StageId = showDetailModel.StageId,
                StartPlaying = showDetailModel.StartPlaying,
                EndPlaying = showDetailModel.EndPlaying,
                LengthOfPlaying = showDetailModel.LengthOfPlaying,
            };
        }

        public static ShowListModel ShowToShowListModel(Show show)
        {
            if (show == null) return null;
            return new ShowListModel
            {
                Id = show.Id,
                StartPlaying = show.StartPlaying,
                EndPlaying = show.EndPlaying,
                LengthOfPlaying = show.LengthOfPlaying,

                ArtistId = show.ArtistId,
                ArtistName = show.Artist.Name,
                ArtistGenre = show.Artist.Genre,

                StageId = show.StageId,
                StageName = show.Stage.Name
            };
        }

        // Used when artist -> show -> artist is causing problems
        public static ShowListModel ShowToShowListModelNoArtist(Show show)
        {
            if (show == null) return null;
            return new ShowListModel
            {
                Id = show.Id,
                StartPlaying = show.StartPlaying,
                EndPlaying = show.EndPlaying,
                LengthOfPlaying = show.LengthOfPlaying,

                StageId = show.StageId,
                StageName = show.Stage.Name
            };
        }

        // Used when stage -> show -> stage is causing problems
        public static ShowListModel ShowToShowListModelNoStage(Show show)
        {
            if (show == null) return null;
            return new ShowListModel
            {
                Id = show.Id,
                StartPlaying = show.StartPlaying,
                EndPlaying = show.EndPlaying,
                LengthOfPlaying = show.LengthOfPlaying,

                ArtistId = show.ArtistId,
                ArtistName = show.Artist.Name,
                ArtistGenre = show.Artist.PhotoPath,
            };
        }

        public static ShowListModel ShowDetailModelToShowListModel(ShowDetailModel showDetailModel)
        {
            if (showDetailModel == null) return null;
            return new ShowListModel
            {
                Id = showDetailModel.Id,
                StartPlaying = showDetailModel.StartPlaying,
                EndPlaying = showDetailModel.EndPlaying,
                LengthOfPlaying = showDetailModel.LengthOfPlaying,

                ArtistId = showDetailModel.ArtistId,
                ArtistName = showDetailModel.ArtistName,
                ArtistGenre = showDetailModel.ArtistGenre,

                StageId = showDetailModel.StageId,
                StageName = showDetailModel.StageName
            };
        }

        public static Show ShowListModelToShow(ShowListModel showListModel)
        {
            if (showListModel == null) return null;
            return new Show
            {
                Id = showListModel.Id,
                ArtistId = showListModel.ArtistId,
                StageId = showListModel.StageId,
                StartPlaying = showListModel.StartPlaying,
                EndPlaying = showListModel.EndPlaying,
                LengthOfPlaying = showListModel.LengthOfPlaying,
            };
        }
    }
}