using EShop.Data.DataCore;
using EShop.Data.Repository;
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
        IEnumerable<Product> GetAll(string keyword);

        public Product GetProductById(int id);

        public Product Delete(Product product);

        void SaveChanges();

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

        public Product Delete(Product product)
        {
            return _productRepository.Delete(product);
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return _productRepository.GetAll();
            }
            else
            {
                return _productRepository.GetMulti(x=>x.ProductName.Contains(keyword));
            }
           
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
