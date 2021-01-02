﻿using GHNApi;
using GHNApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/giaohang/[action]")]
    [ApiController]
    public class ClientGiaoHangNhanhController : ControllerBase
    {
        private readonly IGiaoHangNhanhService _giaoHangNhanhService;
        public ClientGiaoHangNhanhController(IGiaoHangNhanhService giaoHangNhanhService)
        {
            _giaoHangNhanhService = giaoHangNhanhService;
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
        public ActionResult<IEnumerable<ShippingFee>> GetShippingFee(int DistrictId,string ward_code)
        {
            try
            {
                var result = _giaoHangNhanhService.GetShippingFee(ward_code,DistrictId);
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
