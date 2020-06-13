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

        [HttpPost]
        public AttributeValue Create(AttributeValue attr)
        {
            var att = _attributeValueService.Add(attr);
            _attributeValueService.SaveChanges();
            return att;
        }

        [HttpGet]
        public AttributeValue GetById(int id)
        {
            return _attributeValueService.GetAttributeValueById(id);
        }

        [HttpDelete]
        public AttributeValue Delete(AttributeValue attribute)
        {
            var oldEntity= _attributeValueService.Delete(attribute);
            _attributeValueService.SaveChanges();
            return oldEntity;
        }

    }
}