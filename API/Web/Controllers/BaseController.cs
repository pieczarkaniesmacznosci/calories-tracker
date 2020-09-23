using System;
using API.Web.Result;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult FromResult<T>(Result<T> result)
        {
            switch (result.ResultType)
            {
                case ResultType.Ok:
                    return base.Ok(result.Data);
                case ResultType.NotFound:
                    return base.NotFound(result.Errors);
                case ResultType.Invalid:
                    return base.BadRequest(result.Errors);
                case ResultType.Unexpected:
                    return base.BadRequest(result.Errors);
                case ResultType.Unauthorized:
                    return base.Unauthorized();
                default:
                    throw new Exception("An unhandled result has occurred as a result of a service call.");
            }
        }
    }
}