﻿using EShop.Server.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("Content")]
    public class Content:SeoAndAudit
    {
        [Key]
     
        public long ID { get; set; }
        [MaxLength(250, ErrorMessage = "Tên tin tức không vượt quá 250 ký tự"), MinLength(1, ErrorMessage = "Độ dài tối thiểu 1 ký tự")]
        [Required(ErrorMessage = "Tên tin tức không được để trống")]
        public string Name { get; set; }

        [MaxLength(250)]
        public string MetaTitle { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }
        public long CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int Warranty { get; set; }


        [MaxLength(250)]
        public string MetaKeywords { get; set; }

        [MaxLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }
        public DateTime TopHot { get; set; }

        public int ViewCount { get; set; }

        [MaxLength(500)]
        public string Tags { get; set; }

        [MaxLength(2)]
        public string Language { get; set; }
    }
}
