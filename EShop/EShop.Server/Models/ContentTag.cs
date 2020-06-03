using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("ContentTag")]
    public class ContentTag 
    {
        [Column(Order = 0)]
      
        public long ContentID { get; set; }
        [Column(Order = 1)]
        [MaxLength(50)]
        public string TagID { get; set; }
    }
}
