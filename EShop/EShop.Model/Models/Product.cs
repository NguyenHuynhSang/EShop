using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Models
{
    [Table("Product")] // map class với table trong csdl
    public class Product
    {
        [Key] // chỉ định    khóa chính
        public int ID { set; get; }
        [MaxLength(500)] //chỉ định độ dài tối đa, nếu k có mặc định là max
        public string ProductName { set; get; }

        public float UnitPrice { set; get; }

        public int Quantity { set; get; }


    }
}
