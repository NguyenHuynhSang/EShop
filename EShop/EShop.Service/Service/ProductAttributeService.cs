using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{

    public interface IProductAttributeService
    {
        ProductAttribute Add(ProductAttribute productAttribute);
        IEnumerable<ProductAttribute> GetAll(string keyword);

        public ProductAttribute GetProductAttributeById(int id);

        public ProductAttribute Delete(ProductAttribute productAttribute);

        void SaveChanges();

    }
    public class ProductAttributeService : IProductAttributeService
    {
        IProductAttributeRepository _productAttributeRepository;
        IUnitOfWork _unitOfWork;

        public ProductAttributeService(IProductAttributeRepository productAttributeRepository, IUnitOfWork unitOfWork)
        {
            this._productAttributeRepository = productAttributeRepository;
            this._unitOfWork = unitOfWork;

        }
        public ProductAttribute Add(ProductAttribute productAttribute)
        {
            return _productAttributeRepository.Add(productAttribute);
        }

        public ProductAttribute Delete(ProductAttribute productAttribute)
        {
            return _productAttributeRepository.Delete(productAttribute);
        }

        public IEnumerable<ProductAttribute> GetAll(string keyword)
        {
                return _productAttributeRepository.GetAll();
        }


        public ProductAttribute GetProductAttributeById(int id)
        {
            return _productAttributeRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
