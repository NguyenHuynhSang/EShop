using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.FilterModel;
using EShop.Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Server.ViewModels;
using System.Linq;
using EShop.Server.Extension;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using EShop.Server.Extension;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EShop.Server.Dtos.Admin.Product;

namespace EShop.Server.Service
{
    public interface IProductService
    {
        Product Add(Product product);
        //  IEnumerable<Product> GetAll(Params param);

        IEnumerable<ProductForListDto> GeMulty(Params param);

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
            return _productRepository.Add(product);
        }



        public Product Delete(Product product)
        {
            return _productRepository.Delete(product);
        }

        public IEnumerable<ProductForListDto> GeMulty(Params param)
        {

            var query = _productRepository.GetMulti(null, q => q.Include(x => x.Catalog)
            .Include(x => x.ProductVersions)
                .ThenInclude(y => y.ProductVersionImages)
            .Include(x => x.ProductVersions)
                .ThenInclude(y => y.ProductVersionAttributes)
                    .ThenInclude(z => z.AttributeValue)
                    .ThenInclude(t => t.Attribute));

            var productsReturn = query.Select(x => _mapper.Map<ProductForListDto>(x));
            return productsReturn;
        }

        //public IEnumerable<Product> GetAll(Params param)
        //{
        //    ProductFilterModel filterModel = null;
        //    if (!String.IsNullOrEmpty(param.filter))
        //    {

        //        if (!string.IsNullOrEmpty(param.filter))
        //        {
        //            filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(param.filter);
        //        }

        //    }
        //    if (filterModel != null)
        //    {

        //        if (!filterModel.SearchByMultiKeyword)
        //        {
        //            if (!String.IsNullOrEmpty(filterModel.Name))
        //            {
        //                return _productRepository.GetMulti(x => x.Name.Contains(filterModel.Name))
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.ID != null)
        //            {
        //                return _productRepository.GetMulti(x => x.ID == filterModel.ID)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromWeight != null && filterModel.ToWeight != null)
        //            {
        //                return _productRepository.GetMulti(x => x.Weight >= filterModel.FromWeight && x.Weight <= filterModel.ToWeight)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromWeight == null && filterModel.ToWeight != null)
        //            {
        //                return _productRepository.GetMulti(x => x.Weight <= filterModel.ToWeight)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromWeight != null && filterModel.ToWeight == null)
        //            {
        //                return _productRepository.GetMulti(x => x.Weight >= filterModel.FromWeight)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.CatalogID != null)
        //            {
        //                return _productRepository.GetMulti(x => x.CatalogID == filterModel.CatalogID)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }

        //    return _productRepository.GetAll().AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //}

        //public IEnumerable<ProductViewModel> GetAllProductViewModel(Params param)
        //{

        //    ProductFilterModel filterModel = null;
        //    if (!String.IsNullOrEmpty(param.filter))
        //    {

        //        if (!string.IsNullOrEmpty(param.filter))
        //        {
        //            filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(param.filter);
        //        }

        //    }
        //    if (filterModel != null)
        //    {

        //        if (!filterModel.SearchByMultiKeyword)
        //        {
        //            if (!String.IsNullOrEmpty(filterModel.Name))
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.Name.Contains(filterModel.Name))
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.ID != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.ID == filterModel.ID)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromWeight != null && filterModel.ToWeight != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.Weight >= filterModel.FromWeight && x.Product.Weight <= filterModel.ToWeight)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromWeight == null && filterModel.ToWeight != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.Weight <= filterModel.ToWeight)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromWeight != null && filterModel.ToWeight == null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.Weight >= filterModel.FromWeight)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromOriginalPrice != null && filterModel.ToOriginaPrice != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.OriginalPrice >= filterModel.FromOriginalPrice && x.Product.OriginalPrice <= filterModel.ToOriginaPrice)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromOriginalPrice == null && filterModel.ToOriginaPrice != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.OriginalPrice <= filterModel.ToOriginaPrice)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromOriginalPrice != null && filterModel.ToOriginaPrice == null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.OriginalPrice >= filterModel.FromOriginalPrice)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromNumVersion != null && filterModel.ToNumVersion != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Count() >= filterModel.FromNumVersion && x.ProductVersions.Count() <= filterModel.ToNumVersion)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromNumVersion == null && filterModel.ToNumVersion != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Count() <= filterModel.ToNumVersion)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromNumVersion != null && filterModel.ToNumVersion == null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Count() >= filterModel.FromNumVersion)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromPrice != null && filterModel.ToPrice != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Where(y => y.Price >= filterModel.FromPrice.Value).Count() > 0)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromPrice == null && filterModel.ToPrice != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Where(y => y.Price <= filterModel.ToPrice.Value).Count() > 0)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.FromPrice != null && filterModel.ToPrice == null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Where(y => y.Price >= filterModel.FromPrice.Value).Count() > 0)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //            else if (filterModel.CatalogID != null)
        //            {
        //                return _productRepository.GetProductViewModels().Where(x => x.Product.CatalogID == filterModel.CatalogID)
        //                    .AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    return _productRepository.GetProductViewModels().AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        //}

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
