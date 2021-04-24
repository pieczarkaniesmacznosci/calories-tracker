using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Entities;
using API.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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