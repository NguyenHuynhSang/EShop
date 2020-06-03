using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Data
{
    public interface IDbFactory:IDisposable
    {
        EShopDbContext Init();
    }
}
