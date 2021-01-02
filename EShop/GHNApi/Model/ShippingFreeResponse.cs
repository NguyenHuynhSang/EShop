using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    public class ShippingFreeResponse
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public ShippingFee Data { set; get; }
    }
}
