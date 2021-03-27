using Microsoft.AspNetCore.Mvc;
using API_Provinces.Models;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Data;
using API_Provinces.Models.Validations;
using System;

namespace API_Provinces.Controllers
{
    [Route("api/login")]
    [Consumes("application/json")]
    [Produces("text/plain")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        /// <summary>
        ///  This method generate a jwt to authenticate the user
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Return a JWT and 200 code on success and 400 for invalid credentials</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string),200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var _login = new LoginValidation();
                var flag = _login.IsValid(login);
                if (flag)
                {
                    var token = _login.GenerateToken(login);
                    if (token != null)
                    {
                        return Ok(token);
                    }
                    else
                    {
                        return new BadRequestObjectResult("Invalid Credentials");
                    }
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Credentials");
                }
            }
            else
            {
                return new BadRequestObjectResult(HttpStatusCode.BadRequest);
            }

        }
      
    }
}