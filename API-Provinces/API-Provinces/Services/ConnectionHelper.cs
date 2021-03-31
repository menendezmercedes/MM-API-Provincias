using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Provinces.Services
{
    public class ConnectionHelper
    {
        public static string setConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var appSettings = configuration.GetSection("DataBase");
            return "Data Source=" + appSettings.GetValue<string>("Server") + ";Initial Catalog="
                    + appSettings.GetValue<string>("Catalog") + ";Integrated Security=" + appSettings.GetValue<string>("IntegratedSecurity") + ";";
        }
    }
}
