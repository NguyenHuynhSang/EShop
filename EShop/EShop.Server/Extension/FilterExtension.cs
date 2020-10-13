using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace EShop.Server.Extension
{
    public static class FilterExtension
    {
        public static string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        public static Dictionary<string, string> NumberFilterOperator
            = new Dictionary<string, string>
        {
           { "equal","{0}= @0" },
           { "notequal","{0}!= @0" },
           { "greaterthan","{0}> @0" },
           { "greaterthanorequal","{0}>= @0" },
           { "lessthan","{0}< @0" },
           { "lessthanorequal","{0}<= @0" },
           { "inrange","{0}>= @0 and {1}<=@1" },
        };

        public static Dictionary<string, string> TextFilterOperator
           = new Dictionary<string, string>
       {
           { "partialmatch","{0}.ToLower().Contains(@0)" },
           { "notpartialmatch","!{0}.ToLower().Contains(@0)" },
           { "equals","{0}.Equals(@0)" },
           { "notequal","!{0}.Equals(@0)" },
           { "contains","{0}.ToLower().Contains(@0)" },
           { "notcontains","!{0}.ToLower().Contains(@0)" },
           { "startswith","{0}.StartsWith(@0)" },
           { "endswith","{0}.EndsWith(@0)" },
       };

        public static Dictionary<string, string> DateFilterOperator
        = new Dictionary<string, string>
    {
           { "today","DateTime.ParseExact(DateTime.Now,\"yyyy-MM-dd\", System.Globalization.CultureInfo.InvariantCulture)==@0" },
           { "lessthan","{0}< @0" },
           { "greaterthan","{0}> @0" },
    };

        public static IQueryable<TSource> WhereTo<TSource>(this IQueryable<TSource> source, Params param)
        {
   
            var typeOfSource = source.First().GetType();
            string operatorSyntax = "";
            foreach (var item in typeOfSource.GetProperties())
            {
                
                if (item.Name.ToLower()==param.filterProperty.ToLower())
                {
                    if (item.PropertyType==typeof(int))
                    {
                         operatorSyntax = FilterExtension.NumberFilterOperator[param.filterOperator.ToLower()];
                    }
                    else if (item.PropertyType == typeof(String))
                    {
                        operatorSyntax = FilterExtension.TextFilterOperator[param.filterOperator.ToLower()];
                    }
                    else if (item.PropertyType == typeof(DateTime?))
                    {
                        operatorSyntax = FilterExtension.DateFilterOperator[param.filterOperator.ToLower()];
                    }
                   
                    break;
                }
            }
           

            var decodeValue = System.Web.HttpUtility.UrlDecode(param.filterValue);
            var values = decodeValue.Split(',');
            string predicate = operatorSyntax;
            //if (values.Length > 1)
            //{
            //    predicate = String.Format(operatorSyntax, param.filterProperty, param.filterProperty);
            //    ///SMELL-CODE
            //    return source.Where(predicate, values[0], values[1]);
            //}
            predicate = String.Format(operatorSyntax, param.filterProperty);
            return source.Where(predicate, values[0]);


        }
    }





}
