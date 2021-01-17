using EShop.Server.Client.Service;
using EShop.Server.Data.Repository.Address;
using GHNApi;
using GHNApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/giaohang/[action]")]
    [ApiController]
    public class ClientGiaoHangNhanhController : ControllerBase
    {
        private readonly IGiaoHangNhanhService _giaoHangNhanhService;
        private readonly IAddressService _addressService;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ShipType
        {
            SAVING=1,
            STANDARD,
            FAST
        }
        public ClientGiaoHangNhanhController(IGiaoHangNhanhService giaoHangNhanhService, IAddressService addressService)
        {
            _giaoHangNhanhService = giaoHangNhanhService;
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Province>> GetProvinces()
        {
            try
            {
                var result = _giaoHangNhanhService.GetProvince();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<District>> GetDistrictByProvinceId(int ProvinceId)
        {
            try
            {
                var result = _giaoHangNhanhService.GetDistricFromProvinceId(ProvinceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ward>> GetWardByDistrictId(int DistrictId)
        {
            try
            {
                var result = _giaoHangNhanhService.GetWardByDistrictId(DistrictId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<ShippingService>> GetSupportedShippingService(int DistrictId)
        {
            try
            {
                var result = _giaoHangNhanhService.GetSupportedShippingService(DistrictId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShippingFee>> GetShippingFee(string ward_code)
        {
            try
            {
                var DistrictId = _addressService.GetDistrictByWardCode(ward_code);
                var result = _giaoHangNhanhService.GetShippingFee(ward_code,DistrictId, (int)ShipType.STANDARD);
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
