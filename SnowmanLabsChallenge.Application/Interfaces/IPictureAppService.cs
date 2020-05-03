namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using System;

    /// <summary>
    ///     Interface de contrato de Picture Application Service
    /// </summary>
    public interface IPictureAppService : IBaseAppService<PictureViewModel, PictureFilter, Picture>
    {
        void Remove(int id, Guid requesterId, bool commit = true);
    }
}
