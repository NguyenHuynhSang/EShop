using EShop.Server.Client.Dtos.Order;
using EShop.Server.Client.Service;
using EShop.Server.Models;
using GHNApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
namespace EShop.Server.Client.Controller
{
    [Authorize]
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientCheckOutController : ControllerBase
    {
        private readonly IOrderClientService _orderClientService;
        private readonly IGiaoHangNhanhService _giaoHangNhanhService;
        private readonly IAddressService _addressService;
        public ClientCheckOutController(IOrderClientService orderClientService, IGiaoHangNhanhService giaoHangNhanhService, IAddressService addressService)
        {
            _orderClientService = orderClientService;
            _giaoHangNhanhService = giaoHangNhanhService;
            _addressService = addressService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "AUTHEN")]
        public ActionResult<OrderForCheckOutDto> CheckOut(OrderForCheckOutDto order)
        {
            try
            {
                var adfull= _addressService.GetById(order.AddressId);
                var fee = _giaoHangNhanhService.GetShippingFee(adfull.WardCode, adfull.Ward.DistrictId, 2).total;

                var result= _orderClientService.CheckOut(order, fee,adfull);

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
