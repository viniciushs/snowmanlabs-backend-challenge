namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;
    using System;
    using System.Net;

    /// <summary>
    ///     Controller de TouristSpot.
    /// </summary>
    public class TouristSpotController : BaseController<TouristSpotViewModel, TouristSpotFilter, TouristSpot>
    {
        private new readonly ITouristSpotAppService appService;
        private readonly ICommentAppService commentAppService;

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TouristSpotController"/> class.
        ///     Contrutor padrão do TouristSpotController.
        /// </summary>
        /// <param name="appService">Application de serviço</param>
        /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
        public TouristSpotController(
            ITouristSpotAppService appService,
            ICommentAppService commentAppService,
            ILoggerFactory loggerFactory)
            : base(appService)
        {
            this.appService = appService;
            this.commentAppService = commentAppService;

            this.logger = loggerFactory.CreateLogger<TouristSpotController>();
        }

        [HttpGet("{touristSpotId:int}/comment")]
        public IActionResult Comment([FromRoute]int touristSpotId)
        {
            try
            {
                var filter = new CommentFilter { TouristSpotId = touristSpotId };
                var results = this.commentAppService.GetBy(filter);
                return this.Response(results);
            }
            catch (SnowmanLabsChallengeException slcex)
            {
                return this.Response(slcex);
            }
            catch (Exception ex)
            {
                return this.Response(ex);
            }
        }

        [HttpPost("{touristSpotId:int}/comment")]
        public IActionResult Comment([FromRoute] int touristSpotId, [FromBody] CommentViewModel body)
        {
            try
            {
                if (body != null)
                {
                    body.TouristSpotId = touristSpotId;
                }

                var _added = this.commentAppService.Add(body);
                return this.Response(_added, HttpStatusCode.Created, Messages.SaveSuccess);
            }
            catch (SnowmanLabsChallengeException slcex)
            {
                return this.Response(slcex);
            }
            catch (Exception ex)
            {
                return this.Response(ex);
            }
        }

        [HttpDelete("{touristSpotId:int}/comment/{commentId:int}")]
        public IActionResult Comment([FromRoute] int touristSpotId, [FromRoute] int commentId)
        {
            try
            {
                this.commentAppService.Remove(commentId);
                return this.Response(commentId, HttpStatusCode.OK, Messages.DeleteSuccess);
            }
            catch (SnowmanLabsChallengeException slcex)
            {
                return this.Response(slcex);
            }
            catch (Exception ex)
            {
                return this.Response(ex);
            }
        }
    }
}
