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
    public class AttributeController : ApiControllerBase
    {

        private IAttributeService _attributeService;// service xử dụng
        public AttributeController(IAttributeService attributeService)

        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductAttribute>> GetAll(string keyword)
        {

            try
            {
                var list = _attributeService.GetAll(keyword);

                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public ActionResult<ProductAttribute> Create(ProductAttribute attr)
        {
           
            try
            {
                var att = _attributeService.Add(attr);
                _attributeService.SaveChanges();
                return att;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult<ProductAttribute> GetById(int id)
        {
           
            try
            {
                return _attributeService.GetAttributeById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public ActionResult<ProductAttribute> Delete(ProductAttribute attribute)
        {
           
            try
            {
                var oldEntity = _attributeService.Delete(attribute);
                _attributeService.SaveChanges();
                return oldEntity;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }


    }
}