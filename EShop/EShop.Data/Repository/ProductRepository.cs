using EShop.Data.In;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);
    }
    public class ProductRepository:RepositoryBase<Product>, IProductRepository
    {

      
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
