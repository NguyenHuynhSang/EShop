using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Models;
using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductAttributeController : ControllerBase
    {
        private IProductAttributeService _productAttbuteService;// service xử dụng
        public ProductAttributeController(IProductAttributeService productAttributeService)
           
        {
            _productAttbuteService = productAttributeService;
        }

        [HttpGet]
        public IEnumerable<ProductAttribute> GetAll(string keyword)
        {
            var list = _productAttbuteService.GetAll(keyword);

            return list;
        }

    }
}