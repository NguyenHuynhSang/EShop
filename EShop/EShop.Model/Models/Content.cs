using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;
using System.Text;

namespace EShop.Model.Models
{
    public class Content
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string MetaTitle { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        public int CategoryID { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        public int Warranty { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(250)]
        public string CreateBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(250)]
        public string ModifiedBy { get; set; }
        [MaxLength(250)]
        public string MetaKeywords { get; set; }
        [MaxLength(250)]
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
        public DateTime TopHot { get; set; }
        public int ViewCount { get; set; }
        [MaxLength(250)]
        public string Tags { get; set; }
        [MaxLength(2)]
        public string Language { get; set; }
    }
}
