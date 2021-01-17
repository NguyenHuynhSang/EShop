using GHNApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class AddressToShip
    {
        [Key]
        public int Id { set; get; }
        public string WardCode { set; get; }
        [ForeignKey("WardCode")]
        public virtual Ward Ward { set; get; }
        public string AddressDetail { set; get; }
        public bool isMain { set; get; }
    }
}
