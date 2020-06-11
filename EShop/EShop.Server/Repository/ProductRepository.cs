using EShop.Server.Data;
using EShop.Server.InputModel;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EShop.Server.FilterModel;
using System.Linq.Expressions;

namespace EShop.Server.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);

        IEnumerable<ProductViewModel> GetProductViewModels();
        void CreateProductByProductInputModel(ProductInput productInput);
    }
    public class ProductRepository:RepositoryBase<Product>, IProductRepository
    {

      
        public ProductRepository(EShopDbContext dbContext) : base(dbContext)
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

        public IEnumerable<ProductViewModel> GetProductViewModels()
        {
            var querry = from p in DbContext.Products
                         join c in DbContext.Catalogs
                         on p.CatalogID equals c.ID
                         select new ProductViewModel
                         {
                             Product = p,
                             Catalog = c,
                             ProductVersion = DbContext.ProductVersions.Where(x=>x.ProductID== p.ID)
                         };
            return querry.ToList();
        }
    }
}
