namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEnumMap<TEntity> : BaseMap<TEntity>
        where TEntity : BaseEnumEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None)
                .IsRequired();

            builder.HasIndex(c => c.Code);

            builder.Property(c => c.Code)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValue();

            builder.Property(c => c.Active)
                .HasColumnName("Active")
                .HasDefaultValue(true);

            builder.Property(c => c.Description)
                .HasColumnType("varchar(31)")
                .HasMaxLength(31)
                .IsRequired();
        }
    }
}
