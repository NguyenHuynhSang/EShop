using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ID { set; get; }
        public string ProductName { set; get; }

        public float UnitPrice { set; get; }

        public int Quantity { set; get; }


    }
}
