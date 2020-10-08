using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace EShop.Server.Extension
{
    public static class FilterExtension
    {
        public static Dictionary<string, string> NumberFilterOperator
            = new Dictionary<string, string>
        {
           { "equal","= @0" },
           { "notequal","!= @0" },
           { "greaterthan","> @0" },
           { "greaterthanorequal",">= @0" },
           { "lessthan","< @0" },
           { "lessthanorequal","<= @0" },
           { "inrange","{0}>= @0 and {1}<=@1" },

        };

        public static IQueryable<TSource> WhereTo<TSource>(this IQueryable<TSource> source, Params param)
        {
            var operatorSyntax = FilterExtension.NumberFilterOperator[param.filterOperator.ToLower()];

            var decodeValue = System.Web.HttpUtility.UrlDecode(param.filterValue);
            var values = decodeValue.Split(',');
            string predicate = operatorSyntax;
            if (values.Length > 1)
            {
                predicate = String.Format(operatorSyntax, param.filterProperty, param.filterProperty);
                ///SMELL-CODE
                return source.Where(predicate, values[0], values[1]);
            }
            predicate = param.filterProperty + operatorSyntax;
            return source.Where(predicate, values[0]);


        }
    }





}
