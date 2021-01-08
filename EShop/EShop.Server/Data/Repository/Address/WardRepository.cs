using GHNApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository.Address
{
    public interface IWardRepository : IRepository<Ward>
    {

    }
    public class WardRepository : RepositoryBase<Ward>, IWardRepository
    {
        public WardRepository(EShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
