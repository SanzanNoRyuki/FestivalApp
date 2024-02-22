using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using System;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class ShowTests
    {
        public AreEqualMethod AreEqual = new AreEqualMethod();

        //[Fact]
        [Theory]
        [InlineData("63e7c127-288b-4332-bff0-73e2cceca8d6",
            "Some Artist",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam sit amet sem id enim congue sodales a sed nulla. Morbi sit amet diam volutpat, luctus odio vitae, laoreet mi. Morbi tincidunt vehicula dignissim. Sed quis tincidunt ipsum. Integer rutrum turpis nec elit dignissim, in pellentesque erat iaculis. Curabitur at eleifend neque, ac hendrerit mi. Etiam vitae hendrerit est. Donec eget justo sed ex porttitor eleifend.",
            "Lorem ipsum dolor sit amet",
            "djoicnpdocvmdifhqwipnfekm",
            "Rock",
            "UK")]
        [InlineData("63e7c127-1111-4332-bff0-73e2cceca8d6", null, null, null, null, null, null)]
        public void Map_Basic_Values(Guid artistId, string artistName, string artistLongDescription, string artistShortDescription, string artistPhoto, string artistGenre, string artistCountry)
        {
            var artistEntity = new Artist()
            {
                Id = artistId,
                Name = artistName,
                LongDescription = artistLongDescription,
                ShortDescription = artistShortDescription,
                PhotoPath = artistPhoto,
                Genre = artistGenre,
                Country = artistCountry
            };

            var stageEntity = new Stage()
            {
                Id = Guid.Parse("9955de72-e26b-46b0-9991-cf19406d8a92"),
                Name = "Some stage",
                Description = "Sed quis tincidunt ipsum. Integer rutrum turpis nec elit dignissim, in pellentesque erat iaculis.",
                PhotoPath = "sdjaifodnofnicofd"
            };

            var showEntity = new Show()
            {
                Id = Guid.Parse("545b973e-afc4-4d9c-9057-06cd1daf31fe"),
                StartPlaying = DateTime.Now,
                EndPlaying = DateTime.Now.AddMinutes(90),
                LengthOfPlaying = DateTime.Now.AddMinutes(90) - DateTime.Now,
                ArtistId = artistEntity.Id,
                Artist = artistEntity,
                StageId = stageEntity.Id,
                Stage = stageEntity
            };

            var artistOSDetail = ShowMapper.ShowToShowDetailModel(showEntity);
            var artistOSList = ShowMapper.ShowToShowListModel(showEntity);
            var artistOSDetailToList = ShowMapper.ShowDetailModelToShowListModel(artistOSDetail);
            var newArtistOSEntity = ShowMapper.ShowListModelToShow(artistOSList);

            AreEqual.Equals(showEntity.Id, artistOSDetail.Id, artistOSList.Id, artistOSDetailToList.Id, newArtistOSEntity.Id);
            AreEqual.Equals(showEntity.StageId, artistOSDetail.StageId, artistOSList.StageId, artistOSDetailToList.StageId, newArtistOSEntity.StageId);
            AreEqual.Equals(showEntity.ArtistId, artistOSDetail.ArtistId, artistOSList.ArtistId, artistOSDetailToList.ArtistId, newArtistOSEntity.ArtistId);
            AreEqual.Equals(showEntity.EndPlaying, artistOSDetail.EndPlaying, artistOSList.EndPlaying, artistOSDetailToList.EndPlaying, newArtistOSEntity.EndPlaying);
            AreEqual.Equals(showEntity.LengthOfPlaying, artistOSDetail.LengthOfPlaying, artistOSList.LengthOfPlaying, artistOSDetailToList.LengthOfPlaying, newArtistOSEntity.LengthOfPlaying);

            var listNoStage = ShowMapper.ShowToShowListModelNoStage(showEntity);
            var listNoArtist = ShowMapper.ShowToShowListModelNoArtist(showEntity);

            AreEqual.Equals(showEntity.Id, listNoArtist.Id, listNoStage.Id);
            AreEqual.Equals(showEntity.LengthOfPlaying, listNoArtist.LengthOfPlaying, listNoStage.LengthOfPlaying);
            Assert.Equal(showEntity.Artist.Name, listNoStage.ArtistName);
        }
    }
}
