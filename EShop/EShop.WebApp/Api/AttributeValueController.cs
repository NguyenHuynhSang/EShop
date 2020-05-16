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
    public class AttributeValueController : ApiBaseController
    {
        private IAttributeValueService _attributeValueService;// service xử dụng
        public AttributeValueController(IAttributeValueService attributeValueService, IErrorService errorService)
           : base(errorService)
        {
            _attributeValueService = attributeValueService;
        }

        [HttpGet]
        public IEnumerable<AttributeValue> GetAll(string keyword)
        {
            var list = _attributeValueService.GetAll(keyword);

            return list;
        }

    }
}