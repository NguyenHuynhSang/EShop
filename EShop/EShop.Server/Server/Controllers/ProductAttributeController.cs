using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Models;
using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductAttributeController : ApiControllerBase
    {
        private IProductAttributeService _productAttbuteService;// service xử dụng
        public ProductAttributeController(IProductAttributeService productAttributeService)

        {
            _productAttbuteService = productAttributeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EShop.Server.Models.Attribute>> GetAll(string keyword)
        {
            try
            {
                var list = _productAttbuteService.GetAll(keyword);
                return Ok(list);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.ToString());
            }
          
        }

    }
}