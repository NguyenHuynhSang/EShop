using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("Tag")]
    public class Tag
    {
        [Key] // chỉ định    khóa chính
        [MaxLength(50)]

        public String ID { set; get; }
        [MaxLength(50)] //chỉ định độ dài tối đa, nếu k có mặc định là max
        public string TagtName { set; get; }
    }
}
