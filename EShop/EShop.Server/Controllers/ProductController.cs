using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Linq;
using EShop.Server.FilterModel;
using Newtonsoft.Json;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using EShop.Server.Extension;
using System;
using EShop.Server.Dtos.Admin.ProductForList;

namespace EShop.Server.Controllers
{

    [Route("api/[controller]/[action]")]

    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;// service xử dụng

        public ProductController(IProductService productService)

        {
            this._productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductForListDto>> GetAllPaging(string productFilterModelJson, string sortBy, string sort= "desc", int pageNumder = 1, int pageSize = 50)
        {

            Params param = new Params(productFilterModelJson,sort,sortBy,pageNumder,pageSize);
            var list = _productService.GeMulty(param);
            return list.ToList();
        }

    

        [HttpPost]
        public Product CreateProduct(Product product)
        {
            var newProduct = _productService.Add(product);
            _productService.SaveChanges();
            return newProduct;
        }


        [HttpPost]
        public void CreateProductByProductInput()
        {
           // _productService.CreateByProductInput(product);

        }

        [HttpGet]
        public Product GetById(int id)
        {
            return _productService.GetProductById(id);
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