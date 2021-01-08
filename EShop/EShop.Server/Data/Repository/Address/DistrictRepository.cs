using GHNApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository.Address
{
    public interface IDistrictRepository : IRepository<District>
    {

    }
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(EShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
