using GHNApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class Address
    {
        [Key]
        public int Id { set; get; }

        public int CustomerId { set; get; }
        [ForeignKey("CustomerId")]
        public Customer Customer { set; get; }

        public string WardCode { set; get; }
        [ForeignKey("WardCode")]
        public virtual Ward Ward { set; get; }
        public string AddressDetail { set; get; }
        public string isMain { set; get; }

    }
}
