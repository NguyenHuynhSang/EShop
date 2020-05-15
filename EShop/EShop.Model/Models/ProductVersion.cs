using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Model.Models
{
   public class ProductVersion
    {
        [Key] // chỉ định    khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int ProductID { set; get; }

        public int WareHouseID { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }


        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { set; get; }

        public int Quantum { set; get; }

        public int RemainingAmount { set; get; }


        [MaxLength(250)]
        public string SKU { set; get; }

        [MaxLength(250)]
        public string Barcode { set; get; }



    }
}
