using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GHNApi.Model
{
   public class Province
    {
        [Key]
        public int ProvinceID { set; get; }
        public string ProvinceName { set; get; }
        public string Code { set; get; }
    }
}
