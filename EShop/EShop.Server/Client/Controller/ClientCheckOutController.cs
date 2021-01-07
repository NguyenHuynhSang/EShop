using EShop.Server.Client.Dtos.Order;
using EShop.Server.Client.Service;
using EShop.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Controller
{
    [Authorize]
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientCheckOutController : ControllerBase
    {
        private readonly IOrderClientService _orderClientService;
        public ClientCheckOutController(IOrderClientService orderClientService)
        {
            _orderClientService = orderClientService;
        }

        [HttpPost]
        public ActionResult<OrderForCheckOutDto> CheckOut(OrderForCheckOutDto order)
        {
            try
            {
               var result= _orderClientService.CheckOut(order);
                _orderClientService.SaveChange();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }

           
        }

    }
}
