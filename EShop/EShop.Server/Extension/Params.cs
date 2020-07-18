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
        public int? pageNumder { set; get; }
        public int? pageSize { set; get; }

        public Params(string filter, string sort, string sortBy, int? pageNumder = null, int? pageSize = null)
        {
            this.filter = filter;
            this.sort = sort;
            this.sortBy = sortBy;
            this.pageNumder = pageNumder;
            this.pageSize = pageSize;
        }
    }
}
