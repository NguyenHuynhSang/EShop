using EShop.Server.Client.Dtos.Customer;
using EShop.Server.Client.Dtos.Shipping;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Dtos.Order
{
    public class OrderForListDto
    {
        public int Id { set; get; }

        public CustomerForOrderDto Customer { set; get; }
        public int AddressId { set; get; }
        public AddressForViewDto Address { set; get; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string ShipName { set; get; }
        public string ShipPhone { set; get; }
        public string ShipEmail { set; get; }

        public string Note { set; get; }
        public int OrderStatusId { set; get; }
        public OrderStatus Status { set; get; }

        public decimal Total { set; get; }
        public decimal TotalQuantity { set; get; }


        public int ShippingFee { set; get; }
    }

}
