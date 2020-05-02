namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.Extensions.Logging;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Controller de Category.
    /// </summary>
    public class CategoryController : BaseController<CategoryViewModel, CategoryFilter, Category>
    {
        private new readonly ICategoryAppService appService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        ///     Contrutor padrão do CategoryController.
        /// </summary>
        /// <param name="appService">Application de serviço</param>
        /// <param name="loggerFactory">Factory de gerenciamento de logs</param>
        public CategoryController(
            ICategoryAppService appService,
            ILoggerFactory loggerFactory)
            : base(appService)
        {
            this.appService = appService;
            this.logger = loggerFactory.CreateLogger<CategoryController>();
        }
    }
}
