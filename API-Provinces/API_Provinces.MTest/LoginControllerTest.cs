using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_Provinces;
using API_Provinces.Models;
using API_Provinces.Controllers;

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

            var unexpectedResponse = "200";
            LoginController _login = new LoginController();

            _login.Post(user);
            var test = _login.Response;
            Assert.AreNotEqual(unexpectedResponse, test);
        }
        [TestMethod]
        public void Post_ValidCredential()
        {
            var user = new LoginModel
            {
                UserName = "devUser",
                Password = "ThisIsThePassword"
            };
            var expectedResponse = "200";
            LoginController _login = new LoginController();

            _login.Post(user);
            //var test = _login.
            //Assert.AreEqual(expectedResponse, test);
        }
    }
}