using BackEnd_ЛР16_Воробьева_В.Д._241_333.Api.Account.Contract;
using BackEnd_ЛР16_Воробьева_В.Д._241_333.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd_ЛР16_Воробьева_В.Д._241_333.Api.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IOptions<JwtOptions> jwtOptions): ControllerBase
    {
        [HttpPost]
        [Route("token/access")]
        public IActionResult GetAccessToken(Credentials credentials) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, credentials.Login),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims:claims,
                expires:DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
                signingCredentials:creds
                );


            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = stringToken});
        }
    }
}
