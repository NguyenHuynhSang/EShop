using EShop.Data.DataCore;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Repository
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
