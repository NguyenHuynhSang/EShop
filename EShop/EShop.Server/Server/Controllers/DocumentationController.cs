using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
    
namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class __DocumentationController : ControllerBase
    {

        [HttpGet]
        [SwaggerOperationCustom(Summary = "Task các công việc và màn hình", FileName = "time_sheet.html")]
        public void __TimeSheet()
        {

        }

    }
}