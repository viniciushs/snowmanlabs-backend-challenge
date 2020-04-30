namespace SnowmanLabsChallenge.Infra.Data.UoW
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Infra.Data.Context;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultContext context;

        public UnitOfWork(DefaultContext context)
        {
            this.context = context;
        }

        public bool Commit()
        {
            var rowsAffected = this.context.SaveChanges();
            return rowsAffected > 0;
        }

        public async Task CommitAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
