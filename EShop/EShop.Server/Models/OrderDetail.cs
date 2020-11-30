using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { set; get; }
        public int ProductVersionId { set; get; }
        [ForeignKey("ProductVersionId")]
        public ProductVersion ProductVersion { set; get; }

        [Required]
        public int Quantity { set; get; }

        [Required]

        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { set; get; }


        [Required]
        public int OrderId { set; get; }


        [ForeignKey("OrderId")]
        public Order Order { set; get; }

    }
}
