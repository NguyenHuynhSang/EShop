
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.In
{
    public class DbFactory : Disposable, IDbFactory
    {
        EShopDbContext dbContext;
        DbContextOptions<EShopDbContext> Options;
        public EShopDbContext Init()
        {

            var optionsBuilder = new DbContextOptionsBuilder<EShopDbContext>();
            optionsBuilder.UseSqlServer(@"data source=LAPTOP-6KVMDIF8;initial catalog=Bt2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            return dbContext ?? (dbContext = new EShopDbContext(optionsBuilder.Options));

        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
