using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    public class District
    {
        public int DistrictID { set; get; }
        public int ProvinceID { set; get; }
        public string DistrictName { set; get; }
        public string Code { set; get; }
        public int Type { set; get; }
        public int SupportType { set; get; }

    }
}
