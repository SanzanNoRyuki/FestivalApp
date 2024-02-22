using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using System;
using System.Linq;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class StageTests
    {
        public AreEqualMethod AreEqual = new AreEqualMethod();

        [Fact]
        public void Map_Basic_Values()
        {
            AreEqualMethod AreEqual = new AreEqualMethod();
            var guidA = Guid.Parse("1111e0cd-aaaa-aaaa-baf6-3d96cc2cbf2e");
            var artistEntity = new Artist()
            {
                Id = guidA,
                Name = ";;Tohle",
                LongDescription = "VeryLongDescription lol",
                PhotoPath = ";;Jsou",
                ShortDescription = ";;Normální",
                Genre = ";;Data",
                Country = "+ěščřřžýýáííé-*+/"
            };

            var guidF = Guid.Parse("8f8f7350-2558-4548-8176-7d9d327c00f4");
            var festivalEntity = new Festival()
            {
                Id = guidF,
                Title = "Festival Test",
                Start = DateTime.Parse("25 Feb 2021 15:00:00"),
                End = DateTime.Now,
                Capacity = 20
            };

            var guidS = Guid.Parse("4893ad5c-beea-44f0-8d89-8cb23486c8ab");
            var stageEntity = new Stage()
            {
                Id = guidS,
                Name = "New test stage",
                Description = "Something about this stage.",
                PhotoPath = "xscndiocnoindivniov",
                FestivalId = guidF,
                Festival = festivalEntity,
                Shows =
                {
                    new Show()
                    {
                        ArtistId = guidA,
                        Artist = artistEntity,
                        StageId = guidS,
                        Stage = new Stage
                        {
                            Name = "New test stage",
                            Description = "Something about this stage.",
                            PhotoPath = "xscndiocnoindivniov",
                            FestivalId = guidF,
                            Festival = festivalEntity,
                        },
                        StartPlaying = DateTime.Now,
                        EndPlaying = DateTime.Now.AddMilliseconds(69),
                        LengthOfPlaying = TimeSpan.FromMilliseconds(69),

                    }
                }
            };

            var stageDetail = StageMapper.StageToStageDetailModel(stageEntity);
            var stageList = StageMapper.StageToStageListModel(stageEntity);
            var newStageEntity = StageMapper.StageListModelToStage(stageList);
            var newStageList = StageMapper.StageDetailModelToStageListModel(stageDetail);

            for (int i = 0; i < stageEntity.Shows.Count; i++)
            {
                Assert.Equal(stageEntity.Shows.ElementAt(i).Id, stageDetail.Shows.ElementAt(i).Id);
                Assert.Equal(stageEntity.Shows.ElementAt(i).StartPlaying, stageDetail.Shows.ElementAt(i).StartPlaying);
                Assert.Equal(stageEntity.Shows.ElementAt(i).EndPlaying, stageDetail.Shows.ElementAt(i).EndPlaying);
            }

            AreEqual.Equals(stageEntity.Id, newStageEntity.Id, stageDetail.Id, stageList.Id, newStageList.Id);
            AreEqual.Equals(stageEntity.Name, newStageEntity.Name, stageDetail.Name, stageList.Name, newStageList.Name);
            AreEqual.Equals(stageEntity.FestivalId, newStageEntity.FestivalId, stageDetail.FestivalId, stageList.FestivalId, newStageList.FestivalId);

            AreEqual.Equals(stageEntity.Festival.Start, stageList.FestivalStart, newStageList.FestivalStart);
            AreEqual.Equals(stageEntity.Festival.Title, stageList.FestivalTitle, newStageList.FestivalTitle);

            AreEqual.Equals(newStageList.FestivalEnd, stageDetail.FestivalEnd);
        }
    }
}
