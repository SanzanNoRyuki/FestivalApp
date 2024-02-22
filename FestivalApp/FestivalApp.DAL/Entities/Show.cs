using System;

namespace FestivalApp.DAL.Entities
{
    public class Show : EntityBase, IEquatable<Show>
    {
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public Guid StageId { get; set; }
        public Stage Stage { get; set; }
        public DateTime StartPlaying { get; set; }
        public DateTime EndPlaying { get; set; }
        public TimeSpan LengthOfPlaying { get; set; }

        public bool Equals(Show other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   ArtistId.Equals(other.ArtistId) &&
                   Equals(Artist, other.Artist) &&
                   StageId.Equals(other.StageId) &&
                   Equals(Stage, other.Stage) &&
                   StartPlaying.Equals(other.StartPlaying) &&
                   EndPlaying.Equals(other.EndPlaying) &&
                   LengthOfPlaying.Equals(other.LengthOfPlaying);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Show)) return false;
            return Equals((Show)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ArtistId, Artist, StageId, Stage, StartPlaying, EndPlaying, LengthOfPlaying);
        }
    }
}