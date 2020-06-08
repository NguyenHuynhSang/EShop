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
    public class AttributeValueController : ControllerBase
    {
        private IAttributeValueService _attributeValueService;// service xử dụng
        public AttributeValueController(IAttributeValueService attributeValueService)
           
        {
            _attributeValueService = attributeValueService;
        }

        [HttpGet]
        public IEnumerable<AttributeValue> GetAll(string atributeId)
        {
            var list = _attributeValueService.GetAll(atributeId);

            return list;
        }

    }
}