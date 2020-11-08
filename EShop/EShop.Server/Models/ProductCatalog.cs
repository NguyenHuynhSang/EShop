using EShop.Server.Interface;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("ProductCatalog")]
    public class ProductCatalog : IAuditAble,ISeoAble,IActiveAble
    {


        [Key] // chỉ định    khóa chính
        public int Id { set; get; }

        public int? ParentID { set; get; }

        [StringLength(500)]
        public string Name { set; get; }

        public string Image { set; get; }
        public bool IsActive { get ; set ; }
        public string SEOTitle { get ; set ; }
        public string SEOUrl { get ; set ; }
        public string SEODescription { get ; set ; }
        public DateTime? CreatedDate { get ; set ; }
        public string CreatedBy { get ; set ; }
        public DateTime? ModifiedDate { get ; set ; }
        public string ModifiedBy { get ; set ; }
    }
}
