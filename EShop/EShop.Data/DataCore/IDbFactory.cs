using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.DataCore
{
    public interface IDbFactory:IDisposable
    {
        EShopDbContext Init();
    }
}
