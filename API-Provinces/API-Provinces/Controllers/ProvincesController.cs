using API_Provinces.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_Provinces.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/provinces")]
    [ApiController]
    public class ProvincesController: ControllerBase
    {

        #region Get
        /// <summary>
        ///     Method that returns the longitud and laltitude of a province
        /// </summary>
        /// <param name="ProvinceName" in="path" required="true" type="string"> The province name </param>
        ///// <param name="Authorization" in="header" required="true" type="string"> The JWT token. Must be prefixed by "Bearer ".</param>
        //[Authorize]
        [HttpGet]
        
        public IActionResult GetProvince(string ProvinceName)
        {
           
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var appSettings = configuration.GetSection("ApiProvinces");
            var pUrl = appSettings.GetValue<string>("url");
            pUrl = string.Format("{0}?nombre={1}", pUrl, ProvinceName);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pUrl);
                request.Method = "GET";
                using (WebResponse response = request.GetResponse()) 
                {
                    using (Stream stReader = response.GetResponseStream())
                    {
                        if (stReader == null)
                        {
                            var resp = new { Message = "There was an error with the API" };
                            return new BadRequestObjectResult(resp);
                        }
                        else
                        {
                            using (StreamReader objReader = new StreamReader(stReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                dynamic jResponse = JsonConvert.DeserializeObject(responseBody);

                                if (jResponse["cantidad"]!= "0")
                                { 
                                    ProvinceModel responseProvince = new ProvinceModel()
                                    {
                                        lon = jResponse["provincias"][0]["centroide"]["lon"],
                                        lat = jResponse["provincias"][0]["centroide"]["lat"]
                                    };

                                    var forResponse = new { Provincia = ProvinceName, Latitud = responseProvince.lat, Longitud = responseProvince.lon };
                                    
                                    return new OkObjectResult(forResponse);
                                }
                                else
                                { 
                                    return new BadRequestObjectResult("No se encontró ninguna provincia llamada: " + ProvinceName);
                                }
                            }
                        }
                    }
                }

            }
            catch (WebException wEx)
            {
                var resp = new { Message = wEx.Message };
                return new BadRequestObjectResult(resp);
            }
            catch (Exception ex)
            {
                var resp = new { Message = ex.Message };
                return new BadRequestObjectResult(resp);
            }
        }
        #endregion
    }
}
