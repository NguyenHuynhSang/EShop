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
    [Table("Customer")]
    public class Customer : IAuditAble
    {
        [Key]
        public int Id { set; get; }

        public string Name { set; get; }

        [Required]
        public string Phone { set; get; }

        public string Address { set; get; }

        public string Email { set; get; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
