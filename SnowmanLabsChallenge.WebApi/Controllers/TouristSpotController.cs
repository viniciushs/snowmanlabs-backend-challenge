namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
        private readonly IFavoriteAppService favoriteAppService;
        private readonly IPictureAppService pictureAppService;

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
            IFavoriteAppService favoriteAppService,
            IPictureAppService pictureAppService,
            ILoggerFactory loggerFactory)
            : base(appService)
        {
            this.appService = appService;
            this.commentAppService = commentAppService;
            this.favoriteAppService = favoriteAppService;
            this.pictureAppService = pictureAppService;

            this.logger = loggerFactory.CreateLogger<TouristSpotController>();
        }

        [HttpGet("mine")]
        public IActionResult Get()
        {
            try
            {
                var filter = new TouristSpotFilter { OwnerId = this.UserId.Value };
                var results = this.appService.GetBy(filter);
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

        [HttpGet]
        [AllowAnonymous]
        public override IActionResult Get(TouristSpotFilter filter)
        {
            try
            {
                var userId = this.UserId;
                var result = this.appService.GetBy(filter, ts => ts.Pictures);
                return this.Response(result);
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

        [HttpPost]
        public override IActionResult Post([FromBody] TouristSpotViewModel obj)
        {
            if (obj != null && this.UserId.HasValue)
            {
                obj.OwnerId = this.UserId.Value;
            }

            return base.Post(obj);
        }

        [HttpGet("{touristSpotId:int}/comment")]
        [AllowAnonymous]
        public IActionResult Comment([FromRoute]int touristSpotId, [FromQuery] CommentFilter filter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new CommentFilter();
                }

                filter.TouristSpotId = touristSpotId;
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
                    if (this.UserId.HasValue)
                    {
                        body.OwnerId = this.UserId.Value;
                    }

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
                var userId = this.UserId.Value;
                this.commentAppService.Remove(commentId, userId, true);
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

        [HttpGet("{touristSpotId:int}/picture")]
        [AllowAnonymous]
        public IActionResult Picture([FromRoute]int touristSpotId, [FromQuery] PictureFilter filter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new PictureFilter();
                }

                filter.TouristSpotId = touristSpotId;
                var results = this.pictureAppService.GetBy(filter);
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

        [HttpPost("{touristSpotId:int}/picture")]
        public IActionResult Picture([FromRoute] int touristSpotId, [FromBody] PictureViewModel body)
        {
            try
            {
                if (body != null)
                {
                    if (this.UserId.HasValue)
                    {
                        body.OwnerId = this.UserId.Value;
                    }

                    body.TouristSpotId = touristSpotId;
                }

                var _added = this.pictureAppService.Add(body);
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

        [HttpDelete("{touristSpotId:int}/picture/{pictureId:int}")]
        public IActionResult Picture([FromRoute] int touristSpotId, [FromRoute] int pictureId)
        {
            try
            {
                var userId = this.UserId.Value;
                this.pictureAppService.Remove(pictureId, userId, true);
                return this.Response(pictureId, HttpStatusCode.OK, Messages.DeleteSuccess);
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

        [HttpGet("{touristSpotId:int}/favorite")]
        public IActionResult Favorite([FromRoute]int touristSpotId)
        {
            try
            {
                var userId = this.UserId.Value;
                var filter = new FavoriteFilter { TouristSpotId = touristSpotId,   HasPagination = false };
                var results = this.favoriteAppService.GetBy(filter);
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

        [HttpGet("{touristSpotId:int}/favorite/count")]
        [AllowAnonymous]
        public IActionResult FavoriteCount([FromRoute]int touristSpotId)
        {
            try
            {
                var filter = new FavoriteFilter { TouristSpotId = touristSpotId };
                var results = this.favoriteAppService.GetBy(filter);
                var count = results.Page.TotalElements;
                return this.Response(count);
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

        [HttpPost("{touristSpotId:int}/favorite/add")]
        public IActionResult FavoriteAdd([FromRoute] int touristSpotId)
        {
            try
            {
                var body = new FavoriteViewModel {
                    TouristSpotId = touristSpotId
                };

                if (this.UserId.HasValue)
                {
                    body.UserId = this.UserId.Value;
                }

                var _added = this.favoriteAppService.Add(body);
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

        [HttpDelete("{touristSpotId:int}/favorite/remove")]
        public IActionResult FavoriteRemove([FromRoute] int touristSpotId)
        {
            try
            {
                var userId = this.UserId.Value;
                this.favoriteAppService.Remove(touristSpotId, userId);
                return this.Response(touristSpotId, HttpStatusCode.OK, Messages.DeleteSuccess);
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
