using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.ViewModels
{
    public class ContentCategoryViewmodel
    {
        public ContentCategory child { get; set; }
        public ContentCategory parent { get; set; }
    }
}
