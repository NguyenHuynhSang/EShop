using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    public class News
    {
        [Key]
      

        public int Id { set; get; }

        public string Content { set; get; }

        public int categoryID { set; get; }
    }
}
