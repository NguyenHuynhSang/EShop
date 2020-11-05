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
    public class AttributeValueController : ApiControllerBase
    {
        private readonly IAttributeValueService _attributeValueService;// service xử dụng
        public AttributeValueController(IAttributeValueService attributeValueService)

        {
            _attributeValueService = attributeValueService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttributeValue>> GetAll(string atributeId)
        {
            try
            {
                var list = _attributeValueService.GetAll(atributeId);

                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }

        }

        [HttpPost]
        public ActionResult<AttributeValue> Create(AttributeValue attr)
        {

            try
            {
                var att = _attributeValueService.Add(attr);
                _attributeValueService.SaveChanges();
                return att;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult<AttributeValue> GetById(int id)
        {
            try
            {
                return _attributeValueService.GetAttributeValueById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }

        }

        [HttpDelete]
        public ActionResult<AttributeValue> Delete(AttributeValue attribute)
        {

            try
            {
                var oldEntity = _attributeValueService.Delete(attribute);
                _attributeValueService.SaveChanges();
                return oldEntity;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
        }

    }
}