using System;
using System.Collections.Generic;
using Nemesis.Essentials.Design;

namespace FestivalApp.DAL.Entities
{
    public class User : EntityBase, IEquatable<User>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Festival> Festivals { get; set; } = new ValueCollection<Festival>();
        public ICollection<Ticket> Tickets { get; set; } = new ValueCollection<Ticket>();

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Email == other.Email &&
                   Name == other.Name &&
                   AddressId.Equals(other.AddressId) &&
                   Equals(Address, other.Address) &&
                   PhoneNumber == other.PhoneNumber &&
                   Equals(Tickets, other.Tickets);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(User)) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Email, Name, AddressId, Address, PhoneNumber, Festivals, Tickets);
        }
    }
}