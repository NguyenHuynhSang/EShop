using EShop.Server.Dtos.Admin.ProductForList;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Dtos.Admin.ProductForList
{
    public class ProductVersionAttributeDto
    {
        public string AttributeName { get; set; }
        public int AtributeID { get; set; }
        public String AttributeValueName { get; set; }
        public int AttributeValueID { get; set; }



    }
}
