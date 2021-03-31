using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoginController;
using LoginModel;
namespace API_Provinces.Tests
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

            _login.Post(user);

            Assert.Fail();
        }
    }
}
