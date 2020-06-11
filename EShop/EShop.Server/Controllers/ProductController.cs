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
        public ActionResult<PagedList<Product>> GetAll(string filterJson,int pageNumder=1,int pageSize=50)
        {
            ProductFilterModel filterModel = null;
            if (!string.IsNullOrEmpty(filterJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(filterJson);
            }

            var list = _productService.GetAll(filterModel) ;

            return PagedList<Product>.ToPagedList(list, pageNumder, pageSize);
        }

        [HttpGet]
        public ActionResult<PagedList<ProductViewModel>> GetProductAllVersions(string filterJson, int pageNumder = 1, int pageSize = 50)
        {
            ProductFilterModel filterModel = null;
            if (!string.IsNullOrEmpty(filterJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(filterJson);
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

        [HttpDelete]
        public Product DeleteProduct(Product product)
        {
            return _productService.Delete(product);
        }
    }
}