using System;

namespace FestivalApp.DAL.Entities
{
    public class EntityBase : IEquatable<EntityBase>
    {
        public Guid Id { get; init; }

        public bool Equals(EntityBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EntityBase)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}