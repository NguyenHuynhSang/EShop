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
using EShop.Server.Extension;
using System;

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
        public ActionResult<PagedList<Product>> GetAllPaging(string productFilterModelJson, string sortBy, string sort= "desc", int pageNumder = 1, int pageSize = 50)
        {
            ProductFilterModel filterModel = null;
            if (!string.IsNullOrEmpty(productFilterModelJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(productFilterModelJson);
            }

            var list = _productService.GetAll(filterModel);
            switch (sort)
            {
                case "desc":

                    list = sortBy == "name" ? list.Distinct().OrderByWithDirection(x => x.Name, true) : list;
                    list = sortBy == "createdDate" ? list.Distinct().OrderByWithDirection(x => x.CreatedDate, true) : list;
                    break;
                case "asc":
                    list = sortBy == "name" ? list.Distinct().OrderByWithDirection(x => x.Name, false) : list;
                    list = sortBy == "createdDate" ? list.Distinct().OrderByWithDirection(x => x.CreatedDate, false) : list;
                    break;
                default:
                    break;
            }
            return PagedList<Product>.ToPagedList(list, pageNumder, pageSize);
        }

        [HttpGet]
        public ActionResult<PagedList<ProductViewModel>> GetProductAllVersionsPaging(string productFilterModelJson, string sortBy , string sort = "desc", int pageNumder = 1, int pageSize = 50)
        {
            ProductFilterModel filterModel = null;
            var list = _productService.GetAllProductViewModel(filterModel);
            if (!string.IsNullOrEmpty(productFilterModelJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(productFilterModelJson);
            }
            switch (sort)
            {
                case "desc":

                    list = sortBy == "name" ? list.Distinct().OrderByWithDirection(x => x.Product.Name, true) : list;
                    list = sortBy == "createdDate" ? list.Distinct().OrderByWithDirection(x => x.Product.CreatedDate, true) : list;
                    break;
                case "asc":
                    list = sortBy == "name" ? list.Distinct().OrderByWithDirection(x => x.Product.Name, false) : list;
                    list = sortBy == "createdDate" ? list.Distinct().OrderByWithDirection(x => x.Product.CreatedDate, false) : list;
                    break;
                default:
                    break;
            }

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