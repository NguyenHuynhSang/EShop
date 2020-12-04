using System.Security.Claims;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {

        protected static readonly ILog logger = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        [HttpGet]
        protected bool IsUserLoggedIn(int userId)
        {
            return userId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}