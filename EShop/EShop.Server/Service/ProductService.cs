using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.FilterModel;
using EShop.Server.InputModel;
using EShop.Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Server.ViewModels;
using System.Linq;

namespace EShop.Server.Service
{
    public interface  IProductService
    {
        Product Add(Product product);
        IEnumerable<Product> GetAll(ProductFilterModel? filterModel=null);

        IEnumerable<ProductViewModel> GetAllProductViewModel(ProductFilterModel? filterModel = null);

        public Product GetProductById(int id);
        public ProductInput GetProductInputByID(int id);
        public Product Delete(Product product);

        void SaveChanges();

        public void CreateByProductInput(ProductInput productInput);

    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
       

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
           

        }
        public Product Add(Product product)
        {
          return  _productRepository.Add(product);
        }

        public void CreateByProductInput(ProductInput productInput)
        {
            _productRepository.CreateProductByProductInputModel(productInput);
        }

        public Product Delete(Product product)
        {
            return _productRepository.Delete(product);
        }

        public IEnumerable<Product> GetAll(ProductFilterModel? filterModel)
        {
            if (filterModel!=null)
            {

                if (!filterModel.SearchByMultiKeyword)
                {
                    if (!String.IsNullOrEmpty(filterModel.Name))
                    {
                        return _productRepository.GetMulti(x => x.Name.Contains(filterModel.Name));
                    }
                    else if (filterModel.ID!=null)
                    {
                        return _productRepository.GetMulti(x => x.ID == filterModel.ID);
                    }
                    else if (filterModel.FromWeight != null && filterModel.ToWeight != null)
                    {
                        return _productRepository.GetMulti(x => x.Weight>=filterModel.FromWeight && x.Weight<=filterModel.ToWeight);
                    }
                    else if (filterModel.FromWeight == null && filterModel.ToWeight != null)
                    {
                        return _productRepository.GetMulti(x => x.Weight <= filterModel.ToWeight);
                    }
                    else if (filterModel.FromWeight != null && filterModel.ToWeight == null)
                    {
                        return _productRepository.GetMulti(x => x.Weight >= filterModel.FromWeight);
                    }
                    else if (filterModel.CatalogID!=null)
                    {
                        return _productRepository.GetMulti(x => x.CatalogID == filterModel.CatalogID);
                    }
                }
                else
                {
                    
                }
            }
          
            return _productRepository.GetAll();
        }

        public IEnumerable<ProductViewModel> GetAllProductViewModel(ProductFilterModel filterModel = null)
        {

            if (filterModel != null)
            {

                if (!filterModel.SearchByMultiKeyword)
                {
                    if (!String.IsNullOrEmpty(filterModel.Name))
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.Name.Contains(filterModel.Name));
                    }
                    else if (filterModel.ID != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.ID == filterModel.ID);
                    }
                    else if (filterModel.FromWeight != null && filterModel.ToWeight != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.Weight >= filterModel.FromWeight && x.Product.Weight <= filterModel.ToWeight);
                    }
                    else if (filterModel.FromWeight == null && filterModel.ToWeight != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.Weight <= filterModel.ToWeight);
                    }
                    else if (filterModel.FromWeight != null && filterModel.ToWeight == null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.Weight >= filterModel.FromWeight);
                    }
                    else if (filterModel.FromOriginalPrice != null && filterModel.ToOriginaPrice != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.OriginalPrice >= filterModel.FromOriginalPrice && x.Product.OriginalPrice <= filterModel.ToOriginaPrice);
                    }
                    else if (filterModel.FromOriginalPrice == null && filterModel.ToOriginaPrice != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.OriginalPrice <= filterModel.ToOriginaPrice);
                    }
                    else if (filterModel.FromOriginalPrice != null && filterModel.ToOriginaPrice == null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.OriginalPrice >= filterModel.FromOriginalPrice);
                    }
                    else if (filterModel.FromNumVersion != null && filterModel.ToNumVersion != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Count() >= filterModel.FromNumVersion && x.ProductVersions.Count() <= filterModel.ToNumVersion);
                    }
                    else if (filterModel.FromNumVersion == null && filterModel.ToNumVersion != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Count() <= filterModel.ToNumVersion);
                    }
                    else if (filterModel.FromNumVersion != null && filterModel.ToNumVersion == null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Count() >= filterModel.FromNumVersion);
                    }
                    else if (filterModel.FromPrice != null && filterModel.ToPrice != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Where(y=>y.Price >= filterModel.FromPrice.Value).Count()>0);
                    }
                    else if (filterModel.FromPrice == null && filterModel.ToPrice != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Where(y => y.Price <= filterModel.ToPrice.Value).Count() > 0);
                    }
                    else if (filterModel.FromPrice != null && filterModel.ToPrice == null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.ProductVersions.Where(y => y.Price >= filterModel.FromPrice.Value).Count() > 0);
                    }
                    else if (filterModel.CatalogID != null)
                    {
                        return _productRepository.GetProductViewModels().Where(x => x.Product.CatalogID == filterModel.CatalogID);
                    }
                }
                else
                {

                }
            }
            return _productRepository.GetProductViewModels();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public ProductInput GetProductInputByID(int id)
        {
            return _productRepository.GetProductInputByID(id);
        }

        public void SaveChanges()
        {
            _productRepository.Commit();
        }
    }
}
