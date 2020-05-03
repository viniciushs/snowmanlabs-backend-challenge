namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Configuração do Entity Framework para a classe <see cref="Vote"/>.
    /// </summary>
    public class VoteMap : BaseMap<Vote>
    {
        public override void Configure(EntityTypeBuilder<Vote> builder)
        {
            base.Configure(builder);

            builder.ToTable("Vote");

            builder.HasOne(v => v.TouristSpot)
                .WithMany(ts => ts.Votes)
                .HasForeignKey(v => v.TouristSpotId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
