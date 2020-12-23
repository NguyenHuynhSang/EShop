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

        public String Note { set; get; }
        public string Name { set; get; }

        [Required]
        public string Phone { set; get; }

        public string Address { set; get; }

        public string Email { set; get; }

        public DateTime? CreatedDate { get  ; set  ; }
        public string CreatedBy { get  ; set  ; }
        public DateTime? ModifiedDate { get  ; set  ; }
        public string ModifiedBy { get  ; set  ; }

        public ICollection<OrderDetail> OrderDetails { set; get; }
    }
}
