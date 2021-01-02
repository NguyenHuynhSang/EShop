using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    class ShippingServiceResponse
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public IEnumerable<ShippingService> Data { set; get; }
    }
}
