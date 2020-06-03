using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.EShop.Server.Models
{
    [Table("Image")]
    public class Image
    {
        [Key] // chỉ định    khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }



        [MaxLength(500)]
        public string Url { set; get; }


    }
}
