using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.EShop.Server.Models
{
    [Table("ProductVersionImage")]
    public class ProductVersionImage
    {
        [Key] // chỉ định    khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

      
        public int ImageID { set; get; }

        public int ProductVersionID { set; get; }





    }
}
