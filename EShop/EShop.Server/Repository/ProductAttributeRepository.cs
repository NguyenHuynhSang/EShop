using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Repository
{
  
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {
        IEnumerable<ProductAttribute> GetByAlias(string alias);
    }
    public class ProductAttributeRepository : RepositoryBase<ProductAttribute>, IProductAttributeRepository
    {


        public ProductAttributeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ProductAttribute> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
