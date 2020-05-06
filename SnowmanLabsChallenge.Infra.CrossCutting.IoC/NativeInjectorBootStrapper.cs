namespace SnowmanLabsChallenge.Infra.CrossCutting.IoC
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.Services;
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Interfaces;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Services;
    using SnowmanLabsChallenge.Infra.CrossCutting.Identity.Authorization;
    using SnowmanLabsChallenge.Infra.CrossCutting.Identity.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;
    using SnowmanLabsChallenge.Infra.Data.Repository;
    using SnowmanLabsChallenge.Infra.Data.UoW;

    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Authorization Polices

            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            #endregion Authorization Polices

            #region Application

            
            // AddAppService //
            services.AddScoped<IVoteAppService, VoteAppService>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IFavoriteAppService, FavoriteAppService>();
            services.AddScoped<IPictureAppService, PictureAppService>();
            services.AddScoped<ICommentAppService, CommentAppService>();
            services.AddScoped<ITouristSpotAppService, TouristSpotAppService>();

            #endregion Application

            #region Infra - Data

            
            // AddNewRepository //
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITouristSpotRepository, TouristSpotRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DefaultContext>();

            #endregion Infra - Data

            #region Services

            services.AddSingleton<IFileService, FileService>(serviceProvider =>
            {
                return new FileService(configuration);
            });

            #endregion Services

            #region Infra - Identity

            services.AddScoped<IUser, AspNetUser>();

            #endregion Infra - Identity
        }
    }
}
