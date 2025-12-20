using ECommerce.ServiceAbstraction.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class APIBaseController : ControllerBase
    {

        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();


            return Problem(result.Errors);

        }

        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
            {
                if (result.Value == null)
                    return NoContent();
                return Ok(result.Value);
            }
            return Problem(result.Errors);
        }

        private ActionResult Problem(IReadOnlyList<Error> errors)
        {
           if(errors.Count == 0 )
                return Problem(statusCode: 500, title: "An unexpected error occurred.");
           if (errors.All(e=>e.Type == ErrorType.Vailedation))
                return HandleValidationProblem(errors);
           return HandleSingleErrorProblem(errors[0]);
        }

        private ActionResult HandleSingleErrorProblem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Vailedation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: error.Description , type: error.Code );
        }


        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code , error.Description);
            }
            return ValidationProblem(modelState);
        }
    }
}
