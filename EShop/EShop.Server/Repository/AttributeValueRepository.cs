using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Repository
{
   


    public interface IAttributeValueRepository : IRepository<AttributeValue>
    {
        IEnumerable<AttributeValue> GetByAlias(string alias);
    }
    public class AttributeValueRepository : RepositoryBase<AttributeValue>, IAttributeValueRepository
    {


        public AttributeValueRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<AttributeValue> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
