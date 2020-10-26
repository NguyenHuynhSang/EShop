using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Server.ViewModels;
using System.Linq;
using EShop.Server.Extension;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using EShop.Server.Extension;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EShop.Server.Dtos.Admin.ProductForList;
using System.Text.Encodings.Web;

namespace EShop.Server.Service
{
    public interface IProductService
    {
        public Product Add(Product product);
        public Product Update(Product product);
        public IEnumerable<ProductForListDto> GetAll(Params param);



        public Product GetProductById(int id);
        public Product Delete(Product product);

        void SaveChanges();



    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;

        }
        public Product Add(Product product)
        {
            product.CreatedDate = DateTime.Now;
            return _productRepository.Add(product);
        }



        public Product Delete(Product product)
        {
            return _productRepository.Delete(product);
        }

        public Product Update(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            return _productRepository.Update(product);
        }



        public IEnumerable<ProductForListDto> GetAll(Params param)
        {
            var query = _productRepository.GetMulti(null, q => q.Include(x => x.Catalog)
                             .Include(x => x.ProductVersions)
                                 .ThenInclude(y => y.ProductVersionImages)
                             .Include(x => x.ProductVersions)
                                 .ThenInclude(y => y.ProductVersionAttributes)
                                     .ThenInclude(z => z.AttributeValue)
                                     .ThenInclude(t => t.Attribute));

            var productsReturn = query.Select(x => _mapper.Map<ProductForListDto>(x));

            try
            {
                if (!String.IsNullOrEmpty(param.filterProperty))
                {
                    productsReturn = productsReturn.AsQueryable().WhereTo(param);
                }

            }
            catch (Exception e)
            {
                
                return ProductPropertyConverter(productsReturn.AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort), param);
            }
            return ProductPropertyConverter(productsReturn.AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort), param);

        }



        private IEnumerable<ProductForListDto> ProductPropertyConverter(IEnumerable<ProductForListDto> source, Params param)
        {

            if (param.currency != null && param.currency.Value != 0)
            {
                source = source.Select(c => { c.OriginalPrice = c.OriginalPrice / param.currency.Value; return c; });
                source = source.ToList();
                foreach (var product in source)
                {
                    foreach (var productVers in product.ProductVersions)
                    {
                        productVers.Price = productVers.Price / param.currency.Value;
                        productVers.PromotionPrice = productVers.PromotionPrice / param.currency.Value;
                    }
                }
            }



            //convert in front end
            //if (!String.IsNullOrEmpty(param.weight) && param.weight.ToLower() == "lb")
            //{
            //    source = source.Select(c => { c.Weight = (int)Math.Round(c.Weight * 2.20462, 2); return c; }).ToList();
            //}
            return source;

        }


        public Product GetProductById(int id)
        {
            return _productRepository.GetSingleById(id);
        }



        public void SaveChanges()
        {
            _productRepository.Commit();
        }
    }
}
