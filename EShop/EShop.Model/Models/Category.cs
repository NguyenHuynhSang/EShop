using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.EShop.Server.Models
{
    public class Category
    {
        [Key]
       
        public int ID { set; get; }

        public string Name { set; get; }
    }
}
