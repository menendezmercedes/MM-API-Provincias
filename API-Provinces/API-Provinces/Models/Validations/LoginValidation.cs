using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace API_Provinces.Models.Validations
{
    public class LoginValidation: ValidationAttribute
    {
        public bool IsValid(LoginModel login)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var appSettings = configuration.GetSection("AuthorizedUsers");
            var user = new LoginModel
            {
                UserName = appSettings.GetValue<string>("UserName"),
                Password = appSettings.GetValue<string>("Password")
            };
            if (login.Password == user.Password && login.UserName == user.UserName)
            {
                return true;
            }
            else
                return false;
        }

        public string GenerateToken(LoginModel login)
        {
            var expires = DateTime.Now.AddDays(1);
            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, login.UserName) });
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: expires);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;

        }
    }
}
