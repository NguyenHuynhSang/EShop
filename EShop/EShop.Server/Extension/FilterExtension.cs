﻿using EShop.Server.Dtos.Admin.ProductForList;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
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
                   { "today","{0}==@0" },
                   { "lessthan","{0}< @0" },
                   { "greaterthan","{0}> @0" },
            };


        public static Dictionary<string, string> SetFilterOperator
       = new Dictionary<string, string>
           {
                   { "equal","==2" },
                   { "notequal","!= @0" },
           };

        private enum FilterType
        {
            Number = 1,
            Text = 2,
            Date = 3,
            Set = 4
        }

        public static Type GetItemType<T>(this IEnumerable<T> enumerable)
        {
            return typeof(T);
        }

        public static IQueryable<TSource> WhereTo<TSource>(this IQueryable<TSource> source, Params param)
        {

            var typeOfSource = source.First().GetType();
            string operatorSyntax = "";
            switch ((FilterType)param.filterType)
            {
                case FilterType.Number:
                    operatorSyntax = FilterExtension.NumberFilterOperator[param.filterOperator.ToLower()];
                    break;
                case FilterType.Text:
                    operatorSyntax = FilterExtension.TextFilterOperator[param.filterOperator.ToLower()];
                    break;
                case FilterType.Date:
                    operatorSyntax = FilterExtension.DateFilterOperator[param.filterOperator.ToLower()];
                    var formattedPredicate = String.Format(operatorSyntax, param.filterProperty);
                    return source.Where(formattedPredicate, DateTime.ParseExact(param.filterValue, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    break;
                case FilterType.Set:
                    operatorSyntax = FilterExtension.SetFilterOperator[param.filterOperator.ToLower()];
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
                                syntaxExtend += properties[i] + ".Any(x=>x.";
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
                    return source.Where(syntaxExtend);
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
