using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {

        [HttpGet]
        public bool IsUserLoggedIn(int userId)
        {
            return userId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}