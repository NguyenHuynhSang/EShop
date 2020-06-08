using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private IAttributeService _attributeService;// service xử dụng
        public AttributeController(IAttributeService attributeService)
           
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public IEnumerable<Server.Models.Attribute> GetAll(string keyword)
        {
            var list = _attributeService.GetAll(keyword);

            return list;
        }

    }
}