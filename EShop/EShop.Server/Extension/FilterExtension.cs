using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Extension
{
    public static class FilterExtension
    {
        public static Dictionary<string, string> NumberFilterOperator
            = new Dictionary<string, string>
        {
           { "equal","= @0" },
           { "notEqual","!= @0" },
           { "greaterThan","> @0" },
           { "greaterThanOrEqual",">= @0" },
           { "lessThan","< @0" },
           { "lessThanOrEqual","<= @0" },
           { "inRange","> @0 and < @1" },

        };


    }





}
