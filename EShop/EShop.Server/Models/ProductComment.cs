using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    [Table("ProductComment")]
    public class ProductComment : IActiveAble
    {
        [Key]
        public int Id { set; get; }

        public int CustomerId { set; get; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; private set; }
        public int ProductId { set; get; }
        [ForeignKey("ProductId")]
        public Product Product { get; private set; }
        public string Comment { set; get; }
        [Range(0, 5)]
        public int Rating { set; get; }
        public bool HasPurchased { set; get; } = false;
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
