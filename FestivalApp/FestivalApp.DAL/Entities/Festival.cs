using System;
using System.Collections.Generic;
using Nemesis.Essentials.Design;

namespace FestivalApp.DAL.Entities
{
    public class Festival : EntityBase, IEquatable<Festival>
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public uint Capacity { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Stage> Stages { get; set; } = new ValueCollection<Stage>();
        public ICollection<Ticket> Tickets { get; set; } = new ValueCollection<Ticket>();

        public bool Equals(Festival other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Title == other.Title &&
                   Start.Equals(other.Start) &&
                   End.Equals(other.End) &&
                   Capacity == other.Capacity &&
                   UserId.Equals(other.UserId) &&
                   Equals(User, other.User) &&
                   Equals(Stages, other.Stages) &&
                   Equals(Tickets, other.Tickets);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Festival)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(Title);
            hashCode.Add(Start);
            hashCode.Add(End);
            hashCode.Add(Capacity);
            hashCode.Add(UserId);
            hashCode.Add(User);
            hashCode.Add(Stages);
            hashCode.Add(Tickets);
            return hashCode.ToHashCode();
        }
    }
}