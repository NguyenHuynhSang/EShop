using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    [Table("MenuGroup")]
    public class MenuGroup
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
    }
}
