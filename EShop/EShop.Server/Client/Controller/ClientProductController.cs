using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Client.Dtos;
using EShop.Server.Client.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientProductController : ControllerBase
    {
        private readonly IProductClientService _productClientService;
        public ClientProductController(IProductClientService productClientService)
        {
            _productClientService = productClientService;
        }

        [HttpGet]
        public void GetAllProduct()
        {

        }


   

        [HttpGet]
        public ActionResult<ProductVersionForSaleDto> Detail(int id)
        {
            try
            {
                //TEST
                var result = _productClientService.GetProductVersionDetail(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }




    }

}
