using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    public class ProvinceResponse
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public IEnumerable<Province> Data { set; get; }
    }
}
