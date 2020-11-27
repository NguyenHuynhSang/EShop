using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("ProductVersionImage")]
    public class ProductVersionImage
    {
        [Key] // chỉ định    khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { private set; get; }


        public String Url { set; get; }

        public bool IsMain { set; get; }
        public int ProductVersionID { private set; get; }


        [ForeignKey("ProductVersionID")]
        public ProductVersion ProductVersion { private set; get; }

        public string PublicId { get; set; }

    }
}
