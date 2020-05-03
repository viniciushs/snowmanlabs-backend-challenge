namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Configuração do Entity Framework para a classe <see cref="Favorite"/>.
    /// </summary>
    public class FavoriteMap : BaseMap<Favorite>
    {
        public override void Configure(EntityTypeBuilder<Favorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("Favorite");

            builder.HasOne(f => f.TouristSpot)
                .WithMany(ts => ts.Favorites)
                .HasForeignKey(f => f.TouristSpotId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
