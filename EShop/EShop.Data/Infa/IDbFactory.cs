using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.In
{
    public interface IDbFactory:IDisposable
    {
        EShopDbContext Init();
    }
}
