using Ereceipt.Application.Results;
using Ereceipt.Web.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;
using System.Security.Claims;
namespace Ereceipt.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected string GetUsername() => User.Identity.IsAuthenticated ? User.Identity.Name : null;

        protected int GetId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Convert.ToInt32(User.Claims.FirstOrDefault(d => d.Type == "Id").Value);
            }
            return 0;
        }

        protected int GetAdminId()
        {
            if (User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Admin") || User.IsInRole("SAdmin"))
                {
                    return 0;
                }
                return Convert.ToInt32(User.Claims.FirstOrDefault(d => d.Type == "Id").Value);
            }
            return -1;
        }

        protected string GetIpAddress() => HttpContext.Connection.RemoteIpAddress.ToString();

        protected string Role() => User.Identity.IsAuthenticated ? User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.Role).Value : null;

        protected IActionResult Result(object result, string error = "")
        {
            var data = result as Result;
            if (data.OK)
                return ResultOk(data.Data);
            return ResultBadRequest(data.Error);
        }

        protected IActionResult ResultOk(object data, string errorMessage = "")
        {
            if (data is ICollection)
            {
                if ((data as ICollection) == null || (data as ICollection).Count == 0)
                    return Ok(new APINotFoundResponse(errorMessage));
            }
            if (data == null)
                return Ok(new APINotFoundResponse(errorMessage));
            return Ok(new APIOKResponse(data));
        }

        protected IActionResult ResultBadRequest(string error = "")
        {
            return Ok(new APIBadRequestResponse(string.IsNullOrEmpty(error) ? "Model not valid" : error));
        }

        protected IActionResult ResultNotFound(string error = "")
        {
            return Ok(new APINotFoundResponse(string.IsNullOrEmpty(error) ? "Resourse not found" : error));
        }
    }
}