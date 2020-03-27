using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.Model.Models
{
    public class Category
    {
        [Key]
        public int ID { set; get; }

        public string Name { set; get; }
    }
}
