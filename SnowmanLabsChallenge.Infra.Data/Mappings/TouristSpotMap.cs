namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Configuração do Entity Framework para a classe <see cref="TouristSpot"/>.
    /// </summary>
    public class TouristSpotMap : BaseMap<TouristSpot>
    {
        public override void Configure(EntityTypeBuilder<TouristSpot> builder)
        {
            base.Configure(builder);

            builder.ToTable("TouristSpot");

            builder.Property(ts => ts.Name)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(ts => ts.Location)
                .IsRequired();

            builder.HasOne(ts => ts.Category)
                .WithMany(c => c.TouristSpots)
                .HasForeignKey(ts => ts.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
