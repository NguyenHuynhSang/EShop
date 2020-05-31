using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.Model.Models
{
    public class Tag
    {
        [Key]
        [MaxLength(50)]
        public string TagID { get; set; }
        [MaxLength(50)]
        public string TagName{get;set;}
    }
}
