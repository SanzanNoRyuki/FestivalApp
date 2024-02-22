using Nemesis.Essentials.Design;
using System;
using System.Collections.Generic;

namespace FestivalApp.DAL.Entities
{
    public class Stage : EntityBase, IEquatable<Stage>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }

        public Guid FestivalId { get; set; }
        public Festival Festival { get; set; }

        public ICollection<Show> Shows { get; set; } = new ValueCollection<Show>();

        public bool Equals(Stage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Name == other.Name &&
                   Description == other.Description &&
                   PhotoPath == other.PhotoPath &&
                   FestivalId.Equals(other.FestivalId) &&
                   Equals(Festival, other.Festival);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Stage)) return false;
            return Equals((Stage)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name, Description, PhotoPath, FestivalId, Festival, Shows);
        }
    }
}