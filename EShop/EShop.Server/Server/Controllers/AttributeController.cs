using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Extension;
using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Server.Controllers
{
    using static EShop.Server.Extension.FilterExtension;
    using ProductAttribute = EShop.Server.Models.Attribute;
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
        public ActionResult<IEnumerable<ProductAttribute>> GetAll(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort = SortType.desc)
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
                var list = _attributeService.GetAll(param);

                return Ok(list.ToList());
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
                return Ok(att);
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
                return Ok(_attributeService.GetAttributeById(id));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
        }


        [HttpPut]

        public ActionResult<ProductAttribute> Update(Models.Attribute attribute)
        {
            try
            {

                var updatedProductCatalog = _attributeService.Update(attribute);
                _attributeService.SaveChanges();
                return Ok(updatedProductCatalog);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }



        }


        [HttpDelete]
        public ActionResult<ProductAttribute> Delete(ProductAttribute attribute)
        {
           
            try
            {
                var oldEntity = _attributeService.Delete(attribute);
                _attributeService.SaveChanges();
                return Ok(oldEntity);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
        }


    }
}