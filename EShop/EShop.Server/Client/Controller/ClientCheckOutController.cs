using EShop.Server.Client.Dtos.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientCheckOutController : ControllerBase
    {
        public ClientCheckOutController()
        { }

        [HttpGet]
        public ActionResult<OrderForCheckOutDto> CheckOut(OrderForCheckOutDto order)
        {
            

            return Ok(order);
        }

    }
}
