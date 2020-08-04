using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Repository
{
    public interface IProductVersionRepository : IRepository<ProductVersion>
    {


    }

    public class ProductVersionRepository : RepositoryBase<ProductVersion>, IProductVersionRepository
    {


        public ProductVersionRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }


    }
}
