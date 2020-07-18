using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin.Product
{
    public class ProductVersionAttributeDto
    {
        public string AttributeName;
        public int AtributeID;
        public IEnumerable<AttributeValue> AttributeValues;



    }
}
