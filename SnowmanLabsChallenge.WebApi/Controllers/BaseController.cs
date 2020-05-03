namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;

    public abstract class BaseController<TViewModel, TFilter, TEntity> : ApiController
        where TViewModel : BaseViewModel
        where TFilter : BaseFilter
        where TEntity : BaseEntity
    {
        protected readonly IBaseAppService<TViewModel, TFilter, TEntity> appService;

        protected BaseController(IBaseAppService<TViewModel, TFilter, TEntity> appService)
            : base()
        {
            this.appService = appService;
        }

        /// <summary>
        ///     Get the register that has the ID passed as parameter.
        /// </summary>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public virtual IActionResult Get(int id)
        {
            try
            {
                var item = this.appService.GetById(id);
                return this.Response(item);
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

        /// <summary>
        ///     Get the register that has the UUID passed as parameter.
        /// </summary>
        [HttpGet("{guid:guid}")]
        [AllowAnonymous]
        public virtual IActionResult Get(Guid guid)
        {
            try
            {
                var item = this.appService.GetByGuid(guid);
                return this.Response(item);
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

        /// <summary>
        ///     Get all registers.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public virtual IActionResult Get(TFilter filter)
        {
            try
            {
                var result = this.appService.GetBy(filter);
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

        /// <summary>
        ///     Save the register passed as parameter.
        /// </summary>
        /// <param name="obj">
        ///     The register to be salved.
        /// </param>
        /// <returns>
        ///     The object representing the register saved with the ID.
        /// </returns>
        [HttpPost]
        public virtual IActionResult Post([FromBody] TViewModel obj)
        {
            try
            {
                var _added = this.appService.Add(obj);
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

        /// <summary>
        /// Update the register passed as parameter.
        /// </summary>
        /// <param name="obj">
        ///     The register to be salved.
        /// </param>
        /// <returns>
        ///     The object representing the register saved with the ID.
        /// </returns>
        [HttpPut("{id:int}")]
        public virtual IActionResult Put([FromBody] TViewModel obj)
        {
            try
            {
                this.appService.Update(obj);
                return this.Response(obj, HttpStatusCode.OK, Messages.UpdateSuccess);
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

        /// <summary>
        /// Delete the register that has the ID passed as parameter.
        /// </summary>
        /// <param name="id">The ID to search and delete.</param>
        [HttpDelete("{id:int}")]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                this.appService.Remove(id);
                return this.Response(id, HttpStatusCode.OK, Messages.DeleteSuccess);
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
