using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.Server.Abstract
{
    public abstract class Seoable
    {
        [StringLength(500)]
        public string SEOTitle { set; get; }

        [StringLength(500)]
        public string SEOUrl { set; get; }

        [StringLength(500)]
        public string SEODescription { set; get; }
    }
}
