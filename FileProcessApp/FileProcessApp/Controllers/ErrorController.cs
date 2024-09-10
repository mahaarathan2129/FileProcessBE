using FileProcessingApp.Common;
using FileProcessingApp.Models.Dto;
using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Services.Interface;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;


namespace FileProcessingApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseController
    {
        [Route("/error")]
        public ActionResult Error()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            Log.Error(
              "Error Occured : {@StatusCode}, {@ErrorMessage}, {@DateTimeUtc}",
              HttpStatusCode.InternalServerError,
              exception,
              DateTime.UtcNow
              );

            return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<string>.Error("An unexpected error occured", (int)HttpStatusCode.InternalServerError));

        }
    }
}