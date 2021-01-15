using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{
    public interface IAddressRepository : IRepository<EShop.Server.Models.Address>
    {

    }
    public class AddressRepository : RepositoryBase<EShop.Server.Models.Address>, IAddressRepository
    {
        public AddressRepository(EShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
