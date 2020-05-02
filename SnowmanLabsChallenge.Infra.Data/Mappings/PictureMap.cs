namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Configuração do Entity Framework para a classe <see cref="Picture"/>.
    /// </summary>
    public class PictureMap : BaseMap<Picture>
    {
        public override void Configure(EntityTypeBuilder<Picture> builder)
        {
            base.Configure(builder);

            builder.ToTable("Picture");

            builder.Property(c => c.Url)
                .HasColumnType("varchar(2047)")
                .HasMaxLength(2047)
                .IsRequired();

            builder.HasOne(picture => picture.TouristSpot)
                .WithMany(ts => ts.Pictures)
                .HasForeignKey(picture => picture.TouristSpotId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
