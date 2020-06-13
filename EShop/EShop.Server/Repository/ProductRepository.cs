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
    public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);

        IEnumerable<ProductViewModel> GetProductViewModels();
        void CreateProductByProductInputModel(ProductInput productInput);
        ProductInput GetProductInputByID(int id);
    }
    public class ProductRepository:RepositoryBase<Product>, IProductRepository
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

            using (var context = DbContext)
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var newProduct = _mapper.Map<Product>(productInput);
                        newProduct.CreatedDate = DateTime.Now;
                        var productReturn = DbContext.Products.Add(newProduct).Entity;
                        DbContext.SaveChanges();
                        foreach (var item in productInput.Versions)
                        {
                            item.ProductID = productReturn.ID;
                            var newProductVersion = _mapper.Map<ProductVersion>(item);
                            DbContext.ProductVersions.Add(newProductVersion);
                            DbContext.SaveChanges();
                            foreach (var att in item.Attributes)
                            {
                                var newProductAtribute = new ProductAttributeValue();
                                newProductAtribute.ProductVersionID = newProductVersion.ID;
                                newProductAtribute.AttributeValueID = att.AttributeValueID;
                                DbContext.ProductAttributeValues.Add(newProductAtribute);
                                DbContext.SaveChanges();
                            }

                            foreach (var img in item.Images)
                            {
                                var newImage = new ProductVersionImage();
                                newImage.ProductVersionID = newProductVersion.ID;
                                newImage.IsMain = img.IsMain;
                                newImage.Url = img.Url;
                                DbContext.ProductVersionImages.Add(newImage);
                                DbContext.SaveChanges();
                            }

                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Log, handle or absorbe I don't care ^_^
                    }
                }
            }
          

        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }

        public ProductInput GetProductInputByID(int id)
        {
            
            var product = DbContext.Products.SingleOrDefault(x => x.ID == id);
            if (product==null)
            {
                return null;
            }
            var productInput = _mapper.Map<ProductInput>(product);
           
            var version = DbContext.ProductVersions.Where(x => x.ProductID==product.ID);
            List<ProductVersionInput> vers= new List<ProductVersionInput>();
            foreach (var item in version)
            {
                var verInput = _mapper.Map<ProductVersionInput>(item);
                vers.Add(verInput);
            }
            productInput.Versions = vers;
            foreach (var item in productInput.Versions)
            {
                item.Images = DbContext.ProductVersionImages.Where(x => x.ProductVersionID == item.ID).ToList();
                item.Attributes = DbContext.ProductAttributeValues.Where(x => x.ProductVersionID == item.ID).ToList();
            }

            return productInput;

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
