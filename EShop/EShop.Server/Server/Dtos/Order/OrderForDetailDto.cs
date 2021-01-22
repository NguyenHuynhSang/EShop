using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Dtos.Order
{
    public class OrderForDetailDto
    {
        public int Id { set; get; }

        public int CustomerId { set; get; }
     
        public Customer Customer { set; get; }
        public int? AddressToShipId { set; get; }
        public string ShippingDetail { set; get; }
        public DateTime? CreatedDate { get; set; }

        public string ShipName { set; get; }
        public string ShipPhone { set; get; }
        public string ShipEmail { set; get; }

        public string Note { set; get; }
        public int OrderStatusId { set; get; }
        public OrderStatus Status { set; get; }

        public IEnumerable<OrderDetailForView> OrderDetails { set; get; }

        public int Total { set; get; }
        public int ShippingFee { set; get; }


        public class OrderDetailForView
        {
            public int Id { set; get; }
            public int ProductVersionId { set; get; }

            public string ProductName { set; get; }
            public string ProductImage { set; get; }
            public int Quantity { set; get; }
            public decimal Price { set; get; }

        }
    }
}
