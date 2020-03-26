using EShop.Data.In;
using EShop.Data.Repository;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface  IProductService
    {
        void Add(Product product);
        IEnumerable<Product> GetAll();

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
        public void Add(Product product)
        {
            _productRepository.Add(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
