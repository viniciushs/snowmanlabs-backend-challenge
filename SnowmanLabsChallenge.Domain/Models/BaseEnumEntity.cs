namespace SnowmanLabsChallenge.Domain.Models
{
    public abstract class BaseEnumEntity : BaseEntity
    {
        public BaseEnumEntity()
            : base()
        {
        }

        public BaseEnumEntity(int id, string code, string description)
            : base()
        {
            this.Id = id;
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}
