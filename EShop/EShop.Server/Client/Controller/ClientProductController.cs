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
    public class ClientProductController : ControllerBase
    {
        public ClientProductController()
        {

        }

        [HttpGet]
        public void GetAllProduct()
        {

        }


        [HttpGet]
        public void GetAllByCategoryId()
        {

        }
        [HttpGet]
        public void GetAllNew()
        {

        }
        [HttpGet]
        public void GetAllTopSale()
        {

        }

        [HttpGet]
        public void Detail(int id)
        {

        }




    }

}
