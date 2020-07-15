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
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EShop.Server.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);

        IEnumerable<ProductViewModel> GetProductViewModels();
        void CreateProductByProductInputModel(ProductInput productInput);
        ProductInput GetProductInputByID(int id);
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        private IMapper _mapper;
        public ProductRepository(EShopDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this._mapper = mapper;
        }


        /// <summary>
        /// 
        /// Careful
        /// Database.BeginTransaction prevent bad practice when call db.SaveChange() multiple times
        /// </summary>
        /// <param name="productInput"></param>
        public void CreateProductByProductInputModel(ProductInput productInput)
        {

      
        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }

        public ProductInput GetProductInputByID(int id)
        {

            return new ProductInput();

        }

        public IEnumerable<ProductViewModel> GetProductViewModels()
        {
            var query = from p in DbContext.Products
                         join c in DbContext.ProductCatalogs
                         on p.CatalogID equals c.ID
                         select new ProductViewModel
                         {
                             Product = p,
                             Catalog = c,
                             ProductVersions = from ver in DbContext.ProductVersions.Where(x => x.ProductID == p.ID)
                                               select new ProductVersionViewModel
                                               {
                                                   ID = ver.ID,
                                                   Description = ver.Description,
                                                   Barcode = ver.Barcode,
                                                   WareHouseID = ver.WareHouseID,
                                                   Price = ver.Price,
                                                   ProductID = ver.ProductID,
                                                   Quantum = ver.Quantum,
                                                   RemainingAmount = ver.RemainingAmount,
                                                   SKU = ver.SKU,
                                                   ProductVersionImages = DbContext.ProductVersionImages.Where(x => x.ProductVersionID == ver.ID)
                                               }
                         };



            return query.ToList();
        }
    }
}
