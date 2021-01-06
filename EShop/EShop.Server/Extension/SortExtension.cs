using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.Net.Http.Headers;
using EShop.Server.Extension;

public static class SortExtension
{
    public static IOrderedQueryable<TSource> OrderByWithDirection<TSource>
        (this IQueryable<TSource> source,
         string keySelector,
         SortType  sort)
    {
        try
        {
            switch (sort)
            {
                case SortType.desc:
                    return source.OrderBy(keySelector == null ? "0" : keySelector + " descending");
                    break;
                case SortType.asc:
                    return source.OrderBy(keySelector == null ? "0" : keySelector);
                    break;
                default:
                    return source.OrderBy("0");
            }
          

        }
        catch (Exception)
        {
            // catch if feild not exist
            return source.OrderBy("0");
        }
       
    }


}
