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
    ///     Controller de Favorite.
    /// </summary>
    public class FavoriteController : ApiController
    {
        private readonly IFavoriteAppService appService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteController"/> class.
        ///     Contrutor padrão do FavoriteController.
        /// </summary>
        /// <param name="appService">Application de serviço</param>
        /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
        public FavoriteController(
            IFavoriteAppService appService,
            ILoggerFactory loggerFactory)
        {
            this.appService = appService;
            this.logger = loggerFactory.CreateLogger<FavoriteController>();
        }

        [HttpGet("mine")]
        public IActionResult Get()
        {
            try
            {
                var filter = new FavoriteFilter { UserId = this.UserId.Value };
                var results = this.appService.GetBy(filter, f => f.TouristSpot);
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
    }
}
