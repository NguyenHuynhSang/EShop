 using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Linq;
using EShop.Server.InputModel;
using EShop.Server.FilterModel;
using Newtonsoft.Json;
using EShop.Server.Models;
using EShop.Server.ViewModels;

namespace EShop.Server.Controllers
{
  
    [Route("api/[controller]/[action]")]
   
    [ApiController]
    public class ProductController:ControllerBase 
    {
        private IProductService _productService;// service xử dụng

        public ProductController(IProductService productService)
            
        {
            this._productService = productService;
        }

        [HttpGet]
        public ActionResult<PagedList<Product>> GetAllPaging(string productFilterModelJson, int pageNumder=1,int pageSize=50)
        {
            ProductFilterModel filterModel = null;
            if (!string.IsNullOrEmpty(productFilterModelJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(productFilterModelJson);
            }

            var list = _productService.GetAll(filterModel) ;

            return PagedList<Product>.ToPagedList(list, pageNumder, pageSize);
        }

        [HttpGet]
        public ActionResult<PagedList<ProductViewModel>> GetProductAllVersionsPaging(string productFilterModelJson, int pageNumder = 1, int pageSize = 50)
        {
            ProductFilterModel filterModel = null;
            if (!string.IsNullOrEmpty(productFilterModelJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(productFilterModelJson);
            }

            var list = _productService.GetAllProductViewModel(filterModel);

            return PagedList<ProductViewModel>.ToPagedList(list, pageNumder, pageSize);
        }

        [HttpPost]
        public Product CreateProduct(Product product)
        {
            var newProduct = _productService.Add(product);
            _productService.SaveChanges();
            return newProduct;
        }


        [HttpPost]
        public void CreateProductByProductInput(ProductInput product)
        {
             _productService.CreateByProductInput(product);
             
        }

        [HttpGet]
        public Product GetById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpGet]
        public ProductInput GetProductInputById(int id)
        {
            return _productService.GetProductInputByID(id);
        }



        [HttpDelete]
        public Product Delete(Product product)
        {
            var oldEntity = _productService.Delete(product);
            _productService.SaveChanges();
            return oldEntity;
        }
    }
}