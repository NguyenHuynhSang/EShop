using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace EShop.Server.Extension
{
    public class Params
    {
        public string filter { set; get; }
        public string sort { set; get; }

        public string sortBy { set; get; }
        public int? page { set; get; }
        public int? perPage { set; get; }

        public decimal? currency { set; get; }

        public string weight { set; get; }
        public string filterProperty { set; get; }
        public string filterOperator { set; get; }
        public string filterValue { get; set; }

        public Params()
        {
        }
    }
}
