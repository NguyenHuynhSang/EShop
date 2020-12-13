using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{
    public interface IMenuRepository : IRepository<Menu>
    {


    }

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {


        public MenuRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }


    }
}
