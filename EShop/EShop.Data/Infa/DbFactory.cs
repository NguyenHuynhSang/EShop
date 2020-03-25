
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.In
{
    class DbFactory : Disposable, IDbFactory
    {
        EShopDbContext dbContext;
        DbContextOptions<EShopDbContext> options;
        public EShopDbContext Init()
        {
            return dbContext ?? (dbContext = new EShopDbContext(options)); 
        }
    }
}
