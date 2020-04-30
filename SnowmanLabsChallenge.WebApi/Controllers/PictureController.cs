namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.Extensions.Logging;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Controller de Picture.
    /// </summary>
    //public class PictureController : BaseController<PictureViewModel, PictureFilter, Picture>
    //{
    //    private new readonly IPictureAppService appService;
    //    private readonly ILogger logger;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="PictureController"/> class.
    //    ///     Contrutor padrão do PictureController.
    //    /// </summary>
    //    /// <param name="appService">Application de serviço</param>
    //    /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
    //    public PictureController(
    //        IPictureAppService appService,
    //        ILoggerFactory loggerFactory)
    //        : base(appService)
    //    {
    //        this.appService = appService;
    //        this.logger = loggerFactory.CreateLogger<PictureController>();
    //    }
    //}
}
