using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    public class DistrictReponse
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public IEnumerable<District> Data { set; get; }
    }
}
