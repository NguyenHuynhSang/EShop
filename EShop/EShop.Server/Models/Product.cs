using EShop.Server.Interface;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace EShop.Server.Models
{
    [Table("Product")] // map class với table trong csdl


    public class Product : ISeoAble, IAuditAble, IActiveAble
    {
       
        [Key] // chỉ định    khóa chính
     
        public int Id { set; get; }
        [MaxLength(500)] //chỉ định độ dài tối đa, nếu k có mặc định là max

        
        public int CatalogID { set; get; }

        [ForeignKey("CatalogID")]
  

        public ProductCatalog Catalog { set; get; }


        [MaxLength(250)]
        public string Url { set; get; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { set; get; }

        [MaxLength(1000)]
        public string Description { set; get; }

        public string Content { set; get; }

        public int Weight { set; get; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal? OriginalPrice { get; set; }

        public bool Deliver { set; get; }

        public bool ApplyPromotion { set; get; }

        public IEnumerable<ProductVersion> ProductVersions { set; get; }
        public DateTime? CreatedDate { get  ; set  ; }
        public string CreatedBy { get  ; set  ; }
        public DateTime? ModifiedDate { get  ; set  ; }
        public string ModifiedBy { get  ; set  ; }
        public string SEOTitle { get  ; set  ; }
        public string SEOUrl { get  ; set  ; }
        public string SEODescription { get  ; set  ; }
        public bool IsActive { get  ; set  ; }
    }
}
