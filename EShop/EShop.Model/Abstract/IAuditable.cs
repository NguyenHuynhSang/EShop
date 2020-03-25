using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model.Abstract
{
   public interface IAuditable
    {
        DateTime? CreatedDate { set; get; }

    }
}
