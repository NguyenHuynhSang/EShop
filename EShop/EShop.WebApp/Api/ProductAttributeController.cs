using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Model.Models;
using EShop.Service.Service;
using EShop.WebApp.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebApp.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductAttributeController : ApiBaseController
    {
        private IProductAttributeService _productAttbuteService;// service xử dụng
        public ProductAttributeController(IProductAttributeService productAttributeService, IErrorService errorService)
           : base(errorService)
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