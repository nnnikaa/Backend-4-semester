using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd_ЛР16_Воробьева_В.Д._241_333.Api.Something.Controllers
{
    [ApiController]
    [Route("api/some")]
    [Authorize]
    public class SomethingController() : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpGet("dataroles")]
        public IActionResult GetSomeDataWithRoles()
        {
            var name = User.Identity!.Name;

            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            return Ok(new
            {
                SomeData = "Hello, this is response from SomethingController",
                Name = name,
                Role = role,
            });
        }


        [HttpGet("data")]
        public IActionResult GetSomeData()
        {
            var name = User.Identity!.Name;

            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            return Ok(new
            {
                SomeData = "Hello, this is response from SomethingController",
                Name = name,
                Role = role,
            });
        }
    }
}
