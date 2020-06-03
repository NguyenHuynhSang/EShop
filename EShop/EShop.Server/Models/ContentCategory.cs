using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("ContentCategory")]
    public class ContentCategory
    {
        [Key]
      
        public long ID { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string MetaTitle { get; set; }

        public long ParentID { get; set; }

        public int DisplayOrder { get; set; }

        [MaxLength(250)]
        public string SeoTitle { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        [MaxLength(250)]
        public string MetaKeywords { get; set; }

        [MaxLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }

        public bool ShowOnHome { get; set; }

        [MaxLength(2)]
        public string Language { get; set; }
    }
}

