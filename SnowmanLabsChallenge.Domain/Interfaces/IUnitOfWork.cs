namespace SnowmanLabsChallenge.Domain.Interfaces
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
