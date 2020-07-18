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

        public decimal? currency { set; get; }

        public string weight { set; get; }
        public Params()
        {
        }
    }
}
