using GHNApi;
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
    }
}
