using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Entities
{
    public class BasketItem
    {
        public int Id { set; get; }
        public string ProductName { set; get; }
        public decimal Price { set; get; }
        public int Quantity { set; get; }
        public string ImageUrl { set; get; }
        
    }
}
