namespace SnowmanLabsChallenge.Infra.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Mappings;
    using System;
    using System.Linq;

    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
        // AddNewDbSet //
        public DbSet<Vote> Votes { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<TouristSpot> TouristSpots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
            // AddNewMapping //
            modelBuilder.ApplyConfiguration(new VoteMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new FavoriteMap());
            modelBuilder.ApplyConfiguration(new PictureMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new TouristSpotMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedOn") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                    entry.Property("Active").CurrentValue = true;
                    entry.Property("Uuid").CurrentValue = Guid.NewGuid();
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedOn").IsModified = false;
                    entry.Property("Uuid").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}
