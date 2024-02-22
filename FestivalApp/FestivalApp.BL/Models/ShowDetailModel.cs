using System;

namespace FestivalApp.BL.Models
{
    public class ShowDetailModel : ModelBase
    {
        public DateTime StartPlaying { get; set; }
        public DateTime EndPlaying { get; set; }
        public TimeSpan LengthOfPlaying { get; set; }

        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistCountry { get; set; }
        public string ArtistGenre { get; set; }
        public string ArtistShortDescription { get; set; }
        public string ArtistLongDescription { get; set; }
        public string ArtistPhotoPath { get; set; }

        public Guid StageId { get; set; }
        public string StageName { get; set; }
        public string StageDescription { get; set; }
        public string StagePhotoPath { get; set; }
    }
}
