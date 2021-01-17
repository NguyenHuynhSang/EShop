using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Order
{
    public class OrderForCheckOutDto
    {

       
        public int CustomerId { set; get; }

        public string ShipName { set; get; }
        public string ShipPhone { set; get; }
        public string ShipEmail { set; get; }
        
        public int AddressId { set; get; }

        public int ShippingFee { set; get; }
        public string Note { set; get; }

        public IEnumerable<OrderDetailForCheckOutDto> OrderDetails { set; get; }
    }

    public class OrderDetailForCheckOutDto
    {
        [Required]
        public int ProductVersionId { set; get; }


        [Required]
        public int Quantity { set; get; }

        [Required]
        public decimal Price { set; get; }


    }
}
