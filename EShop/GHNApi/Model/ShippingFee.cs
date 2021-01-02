using System;
using System.Collections.Generic;
using System.Text;

namespace GHNApi.Model
{
    public class ShippingFee
    {
        public decimal service_fee { set; get; }
        public decimal total { set; get; }
        public decimal insurance_fee {set;get;}
    }
}
