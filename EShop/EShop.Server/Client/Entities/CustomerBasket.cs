using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public CustomerBasket()
        {
        }
        public string Id { set; get; }
        public List<BasketItem> Items { set; get; } = new List<BasketItem>();
    }
}
