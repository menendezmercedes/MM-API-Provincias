using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_Provinces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API_Provinces.MTest
{
    [TestClass]
    public class ProvincesControllerTest
    {
        [TestMethod]
        public void InexistentProvince_GetProvince()
        {
             ProvincesController provincesGet = new ProvincesController();

            var response = provincesGet.GetProvince("BadProvinceName");
            var badResponse = response as BadRequestObjectResult;

            // assert
            Assert.IsNotNull(badResponse);
            Assert.AreEqual(400, badResponse.StatusCode);
        }
        [TestMethod]
        public void ExistentProvince_GetProvince()
        {
            ProvincesController provincesGet = new ProvincesController();

            var response = provincesGet.GetProvince("Salta");
            var okResponse = response as OkObjectResult;

            // assert
            Assert.IsNotNull(okResponse);
            Assert.AreEqual(200, okResponse.StatusCode);
        }
    }
}
