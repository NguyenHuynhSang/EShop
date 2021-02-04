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
using EShop.Server.Extension.mailer;

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
        private readonly IMailer _mailer;
        public ClientCheckOutController(IOrderClientService orderClientService, IGiaoHangNhanhService giaoHangNhanhService, IAddressService addressService, IMailer mailer)
        {
            _orderClientService = orderClientService;
            _giaoHangNhanhService = giaoHangNhanhService;
            _addressService = addressService;
            _mailer = mailer;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "AUTHEN")]
        public ActionResult<OrderForCheckOutDto> CheckOut(OrderForCheckOutDto order)
        {
            try
            {
                var adfull= _addressService.GetById(order.AddressId);
                decimal fee = 30000;
                try
                {
                     fee = _giaoHangNhanhService.GetShippingFee(adfull.WardCode, adfull.Ward.DistrictId, 2).total;
                }
                catch (Exception ex)
                {

                }
             

                var result= _orderClientService.CheckOut(order, fee,adfull);
                if (!String.IsNullOrEmpty(order.ShipEmail))
                {
                    _mailer.SendEmailAsync(order.ShipEmail,"ESHOP THÔNG TIN ĐƠN HÀNG","ĐÃ MUA HÀNG");
                }
              
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
