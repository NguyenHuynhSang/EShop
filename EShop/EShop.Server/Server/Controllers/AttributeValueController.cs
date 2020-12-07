using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Extension;
using EShop.Server.Models;
using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;
using static EShop.Server.Extension.FilterExtension;

namespace EShop.Server.Server.Controllers
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
        public ActionResult<IEnumerable<AttributeValue>> GetAll(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort = SortType.desc)
        {
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.filterProperty = filterProperty;
                param.filterOperator = filterOperator;
                param.filterValue1 = filterValue1;
                param.filterValue = filterValue;
                param.filterType = filterType;

                var list = _attributeValueService.GetAll(param);

                return Ok(list);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.ToString());
            }

        }


        [HttpGet]
        public ActionResult<IEnumerable<AttributeValue>> GetListByAttributeId(int attributeId, string sortBy, SortType sort = SortType.desc)
        {
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
            
  
                param.filterValue = attributeId.ToString();

                var list = _attributeValueService.GetListByAttributeID(param);

                return Ok(list);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.ToString());
            }

        }


        [HttpPost]
        public ActionResult<AttributeValue> Create(AttributeValue attr)
        {

            try
            {
                var att = _attributeValueService.Add(attr);
                _attributeValueService.SaveChanges();
                return Ok(att);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public ActionResult<AttributeValue> GetById(int id)
        {
            try
            {
                return Ok(_attributeValueService.GetAttributeValueById(id));
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
                return Ok(oldEntity);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.ToString());
            }
        }

    }
}