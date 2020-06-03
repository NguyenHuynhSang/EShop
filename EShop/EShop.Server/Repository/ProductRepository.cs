using EShop.Server.Data;
using EShop.Server.InputModel;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);
        void CreateProductByProductInputModel(ProductInput productInput);
    }
    public class ProductRepository:RepositoryBase<Product>, IProductRepository
    {

      
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void CreateProductByProductInputModel(ProductInput productInput)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
