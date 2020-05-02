namespace SnowmanLabsChallenge.Infra.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Configuração do Entity Framework para a classe <see cref="Comment"/>.
    /// </summary>
    public class CommentMap : BaseMap<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.ToTable("Comment");

            builder.Property(c => c.Content)
                .HasColumnType("varchar(2047)")
                .HasMaxLength(2047)
                .IsRequired();

            builder.HasOne(c => c.TouristSpot)
                .WithMany(ts => ts.Comments)
                .HasForeignKey(c => c.TouristSpotId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
