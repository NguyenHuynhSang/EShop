using EShop.Server.Interface;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    [Table("Menu")] // map class với table trong csdl
    public class Menu : IActiveAble, IAuditAble, IOrderAble
    {
        [Key]
        public int Id { set; get; }

        public string Link { set; get; }

        public string Name { set; get; }
        public string Target { set; get; }




        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int MenuGroupId { set; get; }
        [ForeignKey("MenuGroupId")]
        public MenuGroup MenuGroup { set; get; }
        public int Order { get; set; }
    }
}
