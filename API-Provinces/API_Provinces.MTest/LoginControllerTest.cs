using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_Provinces.Models;
using API_Provinces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API_Provinces.MTest
{
    [TestClass]
    public class LoginControllerTest

    {
        [TestMethod]
        public void Post_InvalidCredential()
        {
            var user = new LoginModel
            {
                UserName = "notValid",
                Password = "notValid"
            };

            LoginController _login = new LoginController();

            var response = _login.Post(user);
            var okResponse = response as BadRequestObjectResult;
          
            // assert
            Assert.IsNotNull(okResponse);
            Assert.AreEqual(400, okResponse.StatusCode);
        }
        [TestMethod]
        public void Post_ValidCredential()
        {
            var user = new LoginModel
            {
                UserName = "devUser",
                Password = "ThisIsThePassword"
            };
            LoginController _login = new LoginController();

            var response = _login.Post(user);
            var okResponse = response as OkObjectResult;

            Assert.IsNotNull(okResponse);
            Assert.AreEqual(200, okResponse.StatusCode);
        }
    }
}