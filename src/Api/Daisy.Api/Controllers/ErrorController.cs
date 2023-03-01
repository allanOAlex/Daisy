using Daisy.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daisy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IErrorService errorService;

        public ErrorController(IErrorService ErrorService)
        {
            errorService = ErrorService;   
        }


        [HttpGet("Dev")]
        public IActionResult Development([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [HttpGet("Error")]
        public IActionResult Error() => Problem();

        [HttpGet("internalServerError")]
        public ActionResult GetServerError()
        {
            errorService.NullReference();
            return Ok();
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFound()
        {
            errorService.NotFound();
            return Ok();
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            errorService.BadRequest();
            return Ok();
        }


    }
}

