using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
            
        }

    }
}