namespace SnowmanLabsChallenge.Infra.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
        // AddNewDbSet //

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
            // AddNewMapping //

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
