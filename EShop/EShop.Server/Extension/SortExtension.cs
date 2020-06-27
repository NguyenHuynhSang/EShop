using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
public static class SortExtension
{
    public static IOrderedQueryable<TSource> OrderByWithDirection<TSource>
        (this IQueryable<TSource> source,
         string keySelector,
         string sort)
    {

        sort = sort.ToLower();
        try
        {
            if (sort=="desc")
            {
                return source.OrderBy(keySelector == null ? "0" : keySelector + " descending");
            }
            else if (sort=="asc")
            {
                return source.OrderBy(keySelector == null ? "0" : keySelector);
            }
            return source.OrderBy("0");

        }
        catch (Exception)
        {
            // catch if feild not exist
            return source.OrderBy("0");
        }
       
    }


}
