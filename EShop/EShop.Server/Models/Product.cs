using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    [Table("Product")] // map class với table trong csdl
    public class Product
    {
       
        [Key] // chỉ định    khóa chính
        public int ID { set; get; }
        [MaxLength(500)] //chỉ định độ dài tối đa, nếu k có mặc định là max

        [Required]
        public long CatalogID { set; get; }


        [MaxLength(250)]
        public string Url { set; get; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public int Weight { set; get; }


        [Column(TypeName = "decimal(18,0)")]
        public decimal? OriginalPrice { get; set; }

        public bool Deliver { set; get; }

        [StringLength(500)]
        public string SEOTitle { set; get; }

        [StringLength(500)]
        public string SEOUrl { set; get; }

        [StringLength(500)]
        public string SEODescription { set; get; }

        public bool ApplyPromotion { set; get; }

    }
}
