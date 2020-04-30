namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.Extensions.Logging;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Controller de Comment.
    /// </summary>
    //public class CommentController : BaseController<CommentViewModel, CommentFilter, Comment>
    //{
    //    private new readonly ICommentAppService appService;
    //    private readonly ILogger logger;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="CommentController"/> class.
    //    ///     Contrutor padrão do CommentController.
    //    /// </summary>
    //    /// <param name="appService">Application de serviço</param>
    //    /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
    //    public CommentController(
    //        ICommentAppService appService,
    //        ILoggerFactory loggerFactory)
    //        : base(appService)
    //    {
    //        this.appService = appService;
    //        this.logger = loggerFactory.CreateLogger<CommentController>();
    //    }
    //}
}
