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


        public String Note { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Phone { set; get; }
        [Required]
        public string Address { set; get; }

        public string Email { set; get; }


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
