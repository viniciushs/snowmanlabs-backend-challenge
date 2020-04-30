namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;

    public abstract class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Uuid);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .UseIdentityColumn();

            builder.Property(c => c.Uuid)
                .HasColumnName("Uuid");

            builder.Property(c => c.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValue();

            builder.Property(c => c.Active)
                .HasColumnName("Active");
        }
    }
}
