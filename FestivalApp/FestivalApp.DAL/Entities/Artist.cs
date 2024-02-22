using Nemesis.Essentials.Design;
using System;
using System.Collections.Generic;

namespace FestivalApp.DAL.Entities
{
    public class Artist : EntityBase, IEquatable<Artist>
    {
        public string Name { get; init; }
        public string Country { get; init; }
        public string Genre { get; init; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PhotoPath { get; set; }

        public ICollection<Show> Shows { get; set; } = new ValueCollection<Show>();

        public bool Equals(Artist other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Name == other.Name &&
                   Country == other.Country &&
                   Genre == other.Genre &&
                   ShortDescription == other.ShortDescription &&
                   LongDescription == other.LongDescription &&
                   PhotoPath == other.PhotoPath;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Artist)) return false;
            return Equals((Artist)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name, Country, Genre, ShortDescription, LongDescription, PhotoPath, Shows);
        }
    }
}