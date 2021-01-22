using EShop.Server.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class Order : IAuditAble
    {

        [Key]
        public int Id { set; get; }

        public int CustomerId { set; get; }
        [ForeignKey("CustomerId")]
        public Customer Customer { set; get; }
        public int? AddressToShipId { set; get; }
        [ForeignKey("AddressToShipId")]
        public AddressToShip AddressToShip { set; get; }
        public string ShippingDetail { set; get; }
        public DateTime? CreatedDate { get  ; set  ; }
        public string CreatedBy { get  ; set  ; }
        public DateTime? ModifiedDate { get  ; set  ; }
        public string ModifiedBy { get  ; set  ; }

        public string ShipName { set; get; }
        public string ShipPhone { set; get; }
        public string ShipEmail { set; get; }

        public string Note { set; get; }
        public int OrderStatusId { set; get; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus Status { set; get; }

        public IEnumerable<OrderDetail> OrderDetails { set; get; }
        
        public int ShippingFee { set; get; }



    }
}
