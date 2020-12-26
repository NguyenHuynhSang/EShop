using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
    }
}
