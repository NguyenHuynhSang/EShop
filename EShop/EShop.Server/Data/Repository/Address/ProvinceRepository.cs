using GHNApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{
    public interface IProvinceRepository : IRepository<Province>
    {

    }
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(EShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
