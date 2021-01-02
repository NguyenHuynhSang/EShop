using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    public class WardResponse
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public IEnumerable<Ward> Data { set; get; }
    }
}
