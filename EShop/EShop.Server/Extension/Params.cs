using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static EShop.Server.Extension.FilterExtension;

namespace EShop.Server.Extension
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortType
    {
        desc=0,
        asc=1
    }
    public class Params
    {
        public string filter { set; get; }
        public SortType sort { set; get; }

        public string sortBy { set; get; }
        public int? page { set; get; }
        public int? perPage { set; get; }

        public decimal? currency { set; get; }

        public string weight { set; get; }
        public string filterProperty { set; get; }
        public FilterOperator filterOperator { set; get; }
        public string filterValue { get; set; }
        public string filterValue1 { get; set; } // use for range
        public FilterType filterType { get; set; }// number, string, set...

        public Params()
        {
        }
    }
}
