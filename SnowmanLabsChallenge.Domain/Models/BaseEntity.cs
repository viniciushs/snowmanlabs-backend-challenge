namespace SnowmanLabsChallenge.Domain.Models
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        public BaseEntity()
        {
        }

        public BaseEntity(int id, Guid uuid, DateTime createdOn, bool active)
        {
            this.Id = id;
            this.Uuid = uuid;
            this.CreatedOn = createdOn;
            this.Active = active;
        }

        /// <summary>
        ///     The class' identificator (ID).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     The class' universal unique identifier
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        ///     Date when the register was created.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     Indicates if the register is active or not. Usually used in Logical Delete.
        /// </summary>
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (ReferenceEquals(null, compareTo))
            {
                return false;
            }

            return this.Uuid.Equals(compareTo.Uuid);
        }

        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() * 907) + this.Uuid.GetHashCode();
        }

        public override string ToString()
        {
            return this.GetType().Name + " [Uuid=" + this.Uuid + "]";
        }

        public BaseEntity Copy()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (BaseEntity)formatter.Deserialize(ms);
            }
        }
    }
}
