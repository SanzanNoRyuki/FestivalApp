using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using System.Linq;

namespace FestivalApp.BL.Mappers
{
    public class StageMapper
    {
        public static StageDetailModel StageToStageDetailModel(Stage stage)
        {
            if (stage == null) return null;
            return new StageDetailModel
            {
                Id = stage.Id,
                Name = stage.Name,
                Description = stage.Description,
                PhotoPath = stage.PhotoPath,

                FestivalId = stage.FestivalId,
                FestivalTitle = stage.Festival.Title,
                FestivalStart = stage.Festival.Start,
                FestivalEnd = stage.Festival.End,
                FestivalCapacity = stage.Festival.Capacity,

                Shows = stage.Shows.Select(ShowMapper.ShowToShowListModelNoArtist).ToList(),
            };
        }

        public static Stage StageDetailModelToStage(StageDetailModel stageDetailModel)
        {
            if (stageDetailModel == null) return null;
            return new Stage
            {
                Id = stageDetailModel.Id,
                Name = stageDetailModel.Name,
                Description = stageDetailModel.Description,
                PhotoPath = stageDetailModel.PhotoPath,

                FestivalId = stageDetailModel.FestivalId,

                Shows = stageDetailModel.Shows.Select(ShowMapper.ShowListModelToShow).ToList(),
            };
        }

        public static StageListModel StageToStageListModel(Stage stage)
        {
            if (stage == null) return null;
            return new StageListModel
            {
                Id = stage.Id,
                Name = stage.Name,

                FestivalId = stage.FestivalId,
                FestivalTitle = stage.Festival.Title,
                FestivalStart = stage.Festival.Start,
                FestivalEnd = stage.Festival.End
            };
        }

        public static StageListModel StageDetailModelToStageListModel(StageDetailModel stageDetailModel)
        {
            if (stageDetailModel == null) return null;
            return new StageListModel
            {
                Id = stageDetailModel.Id,
                Name = stageDetailModel.Name,

                FestivalId = stageDetailModel.FestivalId,
                FestivalTitle = stageDetailModel.FestivalTitle,
                FestivalStart = stageDetailModel.FestivalStart,
                FestivalEnd = stageDetailModel.FestivalEnd
            };
        }

        public static Stage StageListModelToStage(StageListModel stageListModel)
        {
            if (stageListModel == null) return null;
            return new Stage
            {
                Id = stageListModel.Id,
                Name = stageListModel.Name,

                FestivalId = stageListModel.FestivalId,
            };

        }
    }
}