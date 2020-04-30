namespace SnowmanLabsChallenge.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SnowmanLabsChallenge.Application.ViewModels;
    using System;
    using System.Net;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected new IActionResult Response(
            object result = null,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            string message = "")
        {
            var response = new ResponseViewModel()
            {
                Data = result,
                Message = message,
                Success = false
            };

            if (result != null)
            {
                // Handled Error
                if (result.GetType() == typeof(SnowmanLabsChallengeException) || result is Exception)
                {
                    response.Data = null;
                    response.Message = (result as Exception).Message;

                    return new ObjectResult(response)
                    { StatusCode = (int)HttpStatusCode.InternalServerError };
                }

                // Success
                if (result is ResponseViewModel)
                {
                    ((ResponseViewModel)result).Success = true;
                    return new ObjectResult(result)
                    { StatusCode = (int)statusCode };
                }
            }

            response.Success = true;

            return new ObjectResult(response)
            { StatusCode = (int)statusCode };
        }
    }
}
