using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EShop.Model.Models
{
    public class ContentCategory
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public int ParentID { get; set; }
        [MaxLength(250)]
        public string MetaTitle { get; set; }
        [MaxLength(250)]
        public string SeoTitle { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(250)]
        public string CreateBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(250)]
        public string ModifiedBy { get; set; }
        [MaxLength(250)]
        public string MetaKeywords { get; set; }
        [MaxLength(250)]
        public string MetaDescriptions { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
        [MaxLength(10)]
        public string Language { get; set; }
    }
}
