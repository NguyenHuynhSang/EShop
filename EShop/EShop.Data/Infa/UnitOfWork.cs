using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.In
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private EShopDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public EShopDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }
        public void Commit()
        {
            if (dbContext==null)
            {
                dbContext = dbFactory.Init();
            }
            dbContext.SaveChanges();
        }
    }
}
