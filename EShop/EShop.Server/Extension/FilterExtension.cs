using EShop.Server.Dtos.Admin.ProductForList;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EShop.Server.Extension
{
    public static class FilterExtension
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum FilterOperator
        {
            /// <summary>
            /// num filter
            /// </summary>
            equal = 1,
            notEqual = 2,
            greaterThan = 3,
            greaterThanOrEqual = 4,
            lessThan = 5,
            lessThanOrEqual = 6,
            range = 7,

            /// <summary>
            /// Text filter
            /// </summary>
            partialMatch = 100,
            notPartialMatch = 101,
            contains = 104,
            notContains = 105,
            startsWith = 106,
            endsWith = 107,


        }

        private static string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        private static Dictionary<int, string> NumberFilterOperator
            = new Dictionary<int, string>
        {
           { 1,"{0}= @0" },
           { 2,"{0}!= @0" },
           { 3,"{0}> @0" },
           { 4,"{0}>= @0" },
           { 5,"{0}< @0" },
           { 6,"{0}<= @0" },
           { 7,"{0}>= @0 and {1}<=@1" },
        };

        private static Dictionary<int, string> TextFilterOperator
           = new Dictionary<int, string>
       {
           { 100,"{0}.ToLower().Contains(@0)" },
           { 101,"!{0}.ToLower().Contains(@0)" },
           { 1,"{0}.Equals(@0)" },
           { 2,"!{0}.Equals(@0)" },
           { 104,"{0}.ToLower().Contains(@0)" },
           { 105,"!{0}.ToLower().Contains(@0)" },
           { 106,"{0}.StartsWith(@0)" },
           { 107,"{0}.EndsWith(@0)" },
       };

        private static Dictionary<int, string> DateFilterOperator
        = new Dictionary<int, string>
            {
                   { 1,"{0}==@0" },
                   { 201,"{0}< @0" },
                   { 202,"{0}> @0" },
            };


        private static Dictionary<int, string> SetFilterOperator
       = new Dictionary<int, string>
           {
                   { 200,"==@0" },
                   { 201,"!= @0" },
           };


        [JsonConverter(typeof(JsonStringEnumConverter))]
  
        public enum FilterType
        {
            num = 1,
            text = 2,
            date = 3,
            set = 4
        }

        public static Type GetItemType<T>(this IEnumerable<T> enumerable)
        {
            return typeof(T);
        }

        public static IQueryable<TSource> WhereTo<TSource>(this IQueryable<TSource> source, Params param)
        {
            if (String.IsNullOrEmpty(param.filterProperty))
            {
                return source;
            }
            var typeOfSource = source.First().GetType();
            string operatorSyntax = "";
            switch ((FilterType)param.filterType)
            {
                case FilterType.num:
                    operatorSyntax = FilterExtension.NumberFilterOperator[(int)param.filterOperator];
                    break;
                case FilterType.text:
                    operatorSyntax = FilterExtension.TextFilterOperator[(int)param.filterOperator];
                    break;
                case FilterType.date:
                    operatorSyntax = FilterExtension.DateFilterOperator[(int)param.filterOperator];
                    var formattedPredicate = String.Format(operatorSyntax, param.filterProperty);
                    return source.Where(formattedPredicate, DateTime.ParseExact(param.filterValue, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case FilterType.set:
                    operatorSyntax = FilterExtension.SetFilterOperator[(int)param.filterOperator];
                    var properties = param.filterProperty.Split('.');
                    var collection = typeOfSource;
                    var syntaxExtend = "";
                    int counter = 0;
                    for (int i = 0; i < properties.Length; i++)
                    {

                        if (i < properties.Length - 1)
                        {
                            var prop = collection.GetProperty(properties[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            if (prop.PropertyType.Name == typeof(IEnumerable<>).Name)
                            {
                                counter++;
                                syntaxExtend += properties[i] + ".Any(";
                                collection = prop.PropertyType.GetGenericArguments()[0];
                            }
                            else
                            {
                                syntaxExtend += properties[i] + ".";
                            }

                        }
                        else
                        {
                            string extend = "";
                            for (int j = 0; j < counter; j++)
                            {
                                extend += ")";
                            }
                            syntaxExtend += properties[i] + operatorSyntax + extend;
                        }

                    }
                    //careful with child list when using Any
                    return source.Where(syntaxExtend, param.filterValue);
                    // productsReturn.Where(x => x.ProductVersions.Any(x => x.ProductVersionAttributes.Any(x => x.AtributeID == 1)));
                    break;
                default:
                    break;
            }


            string predicate = operatorSyntax;
            if (!String.IsNullOrEmpty(param.filterValue1))
            {
                predicate = String.Format(operatorSyntax, param.filterProperty, param.filterProperty);
                return source.Where(predicate, param.filterValue, param.filterValue1);
            }
            else
            {
                predicate = String.Format(operatorSyntax, param.filterProperty);
            }
            return source.Where(predicate, param.filterValue);


        }
    }


}
