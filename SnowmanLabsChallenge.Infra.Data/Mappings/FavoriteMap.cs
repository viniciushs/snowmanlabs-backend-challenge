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
            
            /*
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(256)")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(c => c.Codigo)
                .HasColumnType("char(3)")
                .HasMaxLength(3);

            builder.Property(c => c.CodigoBacen)
                .HasColumnType("char(4)")
                .HasMaxLength(4);

            builder.HasMany(pais => pais.Estados)
                .WithOne(est => est.Pais)
                .HasForeignKey(est => est.PaisId);
            */
        }
    }
}
