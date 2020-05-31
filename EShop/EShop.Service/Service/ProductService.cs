using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.FilterModel;
using EShop.Model.InputModel;
using EShop.Model.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface  IProductService
    {
        Product Add(Product product);
        IEnumerable<Product> GetAll(ProductFilterModel? filterModel=null);

        public Product GetProductById(int id);
        public Product Delete(Product product);

        void SaveChanges();

        public void CreateByProductInput(ProductInput productInput);

    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;

        }
        public Product Add(Product product)
        {
          return  _productRepository.Add(product);
        }

        public void CreateByProductInput(ProductInput productInput)
        {
            throw new NotImplementedException();
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


        public Product GetProductById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
