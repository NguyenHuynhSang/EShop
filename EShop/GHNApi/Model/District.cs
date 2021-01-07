using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GHNApi.Model
{
    public class District
    {
        [Key]
        public int Id { set; get; }
        public int ProvinceId { set; get; }
        [ForeignKey("ProvinceId")]
        public Province Province { set; get; }
        public string DistrictName { set; get; }
        public string Code { set; get; }
        public int Type { set; get; }
        public int SupportType { set; get; }

    }
}
