using System;

namespace FestivalApp.DAL.Entities
{
    public class Address : EntityBase, IEquatable<Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   AddressLine1 == other.AddressLine1 &&
                   AddressLine2 == other.AddressLine2 &&
                   City == other.City &&
                   State == other.State &&
                   Country == other.Country &&
                   PostalCode == other.PostalCode &&
                   UserId.Equals(other.UserId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), AddressLine1, AddressLine2, City, State, Country, PostalCode, UserId);
        }
    }
}