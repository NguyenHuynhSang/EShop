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
            equal=1,
            notEqual,
            greaterThan,
            greaterThanOrEqual,
            lessThan,
            lessThanOrEqual,
            range,

            /// <summary>
            /// Text filter
            /// </summary>
            contains,
            notContains,
            startsWith ,
            endsWith,


        }

        private static string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        private static Dictionary<FilterOperator, string> NumberFilterOperator
            = new Dictionary<FilterOperator, string>
        {
           { FilterOperator.equal,"{0}= @0" },
           { FilterOperator.notEqual,"{0}!= @0" },
           { FilterOperator.greaterThan,"{0}> @0" },
           { FilterOperator.greaterThanOrEqual,"{0}>= @0" },
           { FilterOperator.lessThan,"{0}< @0" },
           { FilterOperator.lessThanOrEqual,"{0}<= @0" },
           { FilterOperator.range,"{0}>= @0 and {1}<=@1" },
        };

        private static Dictionary<FilterOperator, string> TextFilterOperator
           = new Dictionary<FilterOperator, string>
       {
           { FilterOperator.equal,"{0}.Equals(@0)" },
           { FilterOperator.notEqual,"!{0}.Equals(@0)" },
           { FilterOperator.contains,"{0}.ToLower().Contains(@0)" },
           { FilterOperator.notContains,"!{0}.ToLower().Contains(@0)" },
           { FilterOperator.startsWith,"{0}.StartsWith(@0)" },
           { FilterOperator.endsWith,"{0}.EndsWith(@0)" },
       };

        private static Dictionary<int, string> DateFilterOperator
        = new Dictionary<int, string>
            {
                   { 1,"{0}==@0" },
                   { 201,"{0}< @0" },
                   { 202,"{0}> @0" },
            };


        private static Dictionary<FilterOperator, string> SetFilterOperator
       = new Dictionary<FilterOperator, string>
           {
                   {FilterOperator.equal,"==@0" },
                   {FilterOperator.notEqual,"!= @0" },
           };


        [JsonConverter(typeof(JsonStringEnumConverter))]
  
        public enum FilterType
        {
            num=1,
            text,
            date,
            set,
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
                    operatorSyntax = FilterExtension.NumberFilterOperator[param.filterOperator];
                    break;
                case FilterType.text:
                    operatorSyntax = FilterExtension.TextFilterOperator[param.filterOperator];
                    break;
                case FilterType.date:
                    operatorSyntax = FilterExtension.DateFilterOperator[(int)param.filterOperator];
                    var formattedPredicate = String.Format(operatorSyntax, param.filterProperty);
                    return source.Where(formattedPredicate, DateTime.ParseExact(param.filterValue, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case FilterType.set:
                    operatorSyntax = FilterExtension.SetFilterOperator[param.filterOperator];
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
