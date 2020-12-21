using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Entities
{
    public class BasketItem
    {
        public int ProductVersionId { set; get; }
        public string ProductName { set; get; }
        public decimal SalePrice { set; get; }
        public int Quantity { set; get; }
        public string MainImage { set; get; }
        
    }
}
