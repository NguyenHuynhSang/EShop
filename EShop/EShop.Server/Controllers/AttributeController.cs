using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Controllers
{
    using ProductAttribute = Server.Models.Attribute;
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
        public IEnumerable<ProductAttribute> GetAll(string keyword)
        {
            var list = _attributeService.GetAll(keyword);

            return list;
        }
        [HttpPost]
        public ProductAttribute Create(ProductAttribute attr)
        {
            var att = _attributeService.Add(attr);
            _attributeService.SaveChanges();
            return att;
        }

        [HttpGet]
        public ProductAttribute GetById(int id)
        {
            return _attributeService.GetAttributeById(id);
        }

        [HttpDelete]
        public ProductAttribute Delete(ProductAttribute attribute)
        {
            var oldEntity = _attributeService.Delete(attribute);
            _attributeService.SaveChanges();
            return oldEntity;
        }


    }
}