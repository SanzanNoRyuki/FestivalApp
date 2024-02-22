using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using System;
using System.Linq;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class ArtistTests
    {
        public AreEqualMethod AreEqual = new AreEqualMethod();

        [Fact]
        public void Map_Basic_Values()
        {
            AreEqualMethod AreEqual = new AreEqualMethod();
            var guid = Guid.Parse("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e");
            var stageEntity = new Stage()
            {
                Id = guid
            };
            var artistEntity = new Artist()
            {
                Id = Guid.Parse("1111e0cd-aaaa-aaaa-baf6-3d96cc2cbf2e"),
                Name = ";;Tohle",
                LongDescription = "VeryLongDescription lol",
                PhotoPath = ";;Jsou",
                ShortDescription = ";;Normální",
                Genre = ";;Data",
                Country = "+ěščřřžýýáííé-*+/",
                Shows =
                {
                    new Show()
                    {
                        StartPlaying = DateTime.Now,
                        EndPlaying = DateTime.Now.AddMilliseconds(69),
                        Stage = stageEntity,
                        StageId = guid
                    }
                }
            };

            var artistDetail = ArtistMapper.ArtistToArtistDetailModel(artistEntity);
            var artistDetailList = ArtistMapper.ArtistToArtistListModel(artistEntity);

            AreEqual.Equals(artistEntity.ShortDescription, artistDetail.ShortDescription);
            for (int i = 0; i < artistEntity.Shows.Count; i++)
            {
                Assert.Equal(artistEntity.Shows.ElementAt(i).StartPlaying, artistDetail.Shows.ElementAt(i).StartPlaying);
                Assert.Equal(artistEntity.Shows.ElementAt(i).EndPlaying, artistDetail.Shows.ElementAt(i).EndPlaying);
            }
            AreEqual.Equals(artistEntity.Name, artistDetailList.Name);
            AreEqual.Equals(artistEntity.Genre, artistDetailList.Genre);
        }
    }
}
