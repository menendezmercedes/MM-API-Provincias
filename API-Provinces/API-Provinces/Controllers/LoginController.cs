using Microsoft.AspNetCore.Mvc;
using API_Provinces.Models;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Data;
using API_Provinces.Models.Validations;
using System;
using API_Provinces.Services;

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
                        LogHelper.WriteFileLog("Method: Login. User logged " + login.UserName);
                        LogHelper.WriteDBLog("Method: Login. User logged  " + login.UserName);
                        return new OkObjectResult(token);
                    }
                    else
                    {
                        LogHelper.WriteFileLog("Method: Login. Invalid Credential User- "+login.UserName);
                        LogHelper.WriteDBLog("Method: Login. Invalid Credential User- " + login.UserName);
                        return new BadRequestObjectResult("Invalid Credentials");
                    }
                }
                else
                {
                    LogHelper.WriteFileLog("Method: Login. Invalid Credential User- " + login.UserName);
                    LogHelper.WriteDBLog("Method: Login. Invalid Credential User- " + login.UserName);
                    return new BadRequestObjectResult("Invalid Credentials");
                }
            }
            else
            {
                LogHelper.WriteFileLog("Method: Login. Bad Request");
                LogHelper.WriteDBLog("Method: Login. Bad Request");
                return new BadRequestObjectResult(HttpStatusCode.BadRequest);
            }

           

        }
      
    }
}