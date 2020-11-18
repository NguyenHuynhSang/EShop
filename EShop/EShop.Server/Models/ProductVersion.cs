using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
   public class ProductVersion
    {
        [Key] // chỉ định    khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }


        [Required]
        public int ProductID { set; get; }


        [ForeignKey("ProductID")]
        public Product Product { set; get; }

        

        [MaxLength(500)]
        public string Description { set; get; }


        [Column(TypeName = "decimal(18,0)")]
        [Required]
        public decimal Price { set; get; }


        [Column(TypeName = "decimal(18,0)")]
        public decimal? PromotionPrice { set; get; }

        
        public int Quantity { set; get; }

        [MaxLength(250)]
        public string SKU { set; get; }

        [MaxLength(250)]
        public string Barcode { set; get; }


        public  IEnumerable<ProductVersionAttribute> ProductVersionAttributes { set; get; }

        public IEnumerable<ProductVersionImage> ProductVersionImages { set; get; }


        

    }
}
