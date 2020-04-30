namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.Extensions.Logging;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Controller de Favorite.
    /// </summary>
    //public class FavoriteController : BaseController<FavoriteViewModel, FavoriteFilter, Favorite>
    //{
    //    private new readonly IFavoriteAppService appService;
    //    private readonly ILogger logger;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="FavoriteController"/> class.
    //    ///     Contrutor padrão do FavoriteController.
    //    /// </summary>
    //    /// <param name="appService">Application de serviço</param>
    //    /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
    //    public FavoriteController(
    //        IFavoriteAppService appService,
    //        ILoggerFactory loggerFactory)
    //        : base(appService)
    //    {
    //        this.appService = appService;
    //        this.logger = loggerFactory.CreateLogger<FavoriteController>();
    //    }
    //}
}
