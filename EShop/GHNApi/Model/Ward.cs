using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GHNApi.Model
{
    public class Ward
    {
        [Key]
        public string WardCode { set; get; }


        public int DistrictId { set; get; }
        
        [ForeignKey("DistrictId")]
        public District District { set; get; }

        public string WardName { set; get; }
    }
}
