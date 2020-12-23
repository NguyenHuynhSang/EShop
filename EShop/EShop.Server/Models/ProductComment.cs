using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class ProductComment
    {
        public int id;
        public int CustomerId;
        [ForeignKey("CustomerId")]
        public Customer Customer { get; private set; }
        public string ProductId { set; get; }
        [ForeignKey("ProductId")]
        public Product Product { get; private set; }
        public string Comment { set; get; }
        public float Rating { set; get; }
        public bool HasPurchased { set; get; } = false;
        public DateTime? CreatedDate { get; set; }

    }
}
