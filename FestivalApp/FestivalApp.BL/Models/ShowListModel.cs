using System;

namespace FestivalApp.BL.Models
{
    public class ShowListModel : ModelBase
    {
        public DateTime StartPlaying { get; set; }
        public DateTime EndPlaying { get; set; }
        public TimeSpan LengthOfPlaying { get; set; }

        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistGenre { get; set; }

        public Guid StageId { get; set; }
        public string StageName { get; set; }
    }
}