using EShop.Server.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Repository
{

    public interface IAttributeRepository : IRepository<EShop.Server.Models.Attribute>
    {
     
    }
    public class AttributeRepository : RepositoryBase<EShop.Server.Models.Attribute>, IAttributeRepository
    {
        public AttributeRepository(EShopDbContext dbContext) : base(dbContext)
        {
        }
    }

}
