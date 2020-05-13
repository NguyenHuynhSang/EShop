using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Models
{
    [Table("Catalog")]
    public class Catalog
    {
        [Key] // chỉ định    khóa chính
        public int ID { set; get; }

        public int? ParentID { set; get; }

        [StringLength(500)]
        public string Name;

        [StringLength(500)]
        public string SEOTitle { set; get; }

        [StringLength(500)]
        public string SEOUrl { set; get; }

        [StringLength(500)]
        public string SEODescription { set; get; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }


    }
}
