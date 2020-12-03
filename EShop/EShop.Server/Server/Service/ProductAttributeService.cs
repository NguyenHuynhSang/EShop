using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Service
{
    using ProductAttribute = EShop.Server.Models.Attribute;
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
       

        public ProductAttributeService(IProductAttributeRepository productAttributeRepository)
        {
            this._productAttributeRepository = productAttributeRepository;
           

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
            _productAttributeRepository.Commit();
        }
    }
}
