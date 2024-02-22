using System;
using FestivalApp.DAL.Entities.Enums;

namespace FestivalApp.DAL.Entities
{
    public class Ticket : EntityBase, IEquatable<Ticket>
    {
        public TicketType Type { get; set; }
        public TicketLength Length { get; set; }
        public decimal PriceAmount { get; set; }
        public Currency PriceCurrency { get; set; }
        public DateTime StartDate { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public Guid FestivalId { get; set; }
        public Festival Festival { get; set; }

        public bool Equals(Ticket other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Type == other.Type &&
                   Length == other.Length &&
                   PriceAmount == other.PriceAmount &&
                   PriceCurrency == other.PriceCurrency &&
                   UserId.Equals(other.UserId) &&
                   FestivalId.Equals(other.FestivalId) &&
                   Equals(Festival, other.Festival);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ticket)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add((int)Type);
            hashCode.Add((int)Length);
            hashCode.Add(PriceAmount);
            hashCode.Add((int)PriceCurrency);
            hashCode.Add(UserId);
            hashCode.Add(User);
            hashCode.Add(FestivalId);
            hashCode.Add(Festival);
            return hashCode.ToHashCode();
        }
    }
}