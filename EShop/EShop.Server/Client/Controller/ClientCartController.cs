using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Client.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientCartController : ControllerBase
    {
        private readonly IProductClientService _productClientService;
        public ClientCartController(IProductClientService productClientService)
        {
            _productClientService = productClientService;
        }

        [HttpGet]
        public ActionResult<bool> CheckAvalibleQuantity(int versionId,int quantityToBuy)
        {
            try
            {
                //TEST
                var result = _productClientService.CheckVersionQuantity(versionId, quantityToBuy);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

    }
}