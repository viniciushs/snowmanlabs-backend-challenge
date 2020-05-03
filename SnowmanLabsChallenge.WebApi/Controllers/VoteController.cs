namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.Extensions.Logging;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Controller de Vote.
    /// </summary>
    //public class VoteController : BaseController<VoteViewModel, VoteFilter, Vote>
    //{
    //    private new readonly IVoteAppService appService;
    //    private readonly ILogger logger;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="VoteController"/> class.
    //    ///     Contrutor padrão do VoteController.
    //    /// </summary>
    //    /// <param name="appService">Application de serviço</param>
    //    /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
    //    public VoteController(
    //        IVoteAppService appService,
    //        ILoggerFactory loggerFactory)
    //        : base(appService)
    //    {
    //        this.appService = appService;
    //        this.logger = loggerFactory.CreateLogger<VoteController>();
    //    }
    //}
}
