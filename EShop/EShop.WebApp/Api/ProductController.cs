using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EShop.Model.Models;
using EShop.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
         IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        

        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            var list = _productService.GetAll();
           
            return list;

        }

        [HttpGet]
        public IEnumerable<Product> GetBlaBlaAll()
        {
            var list = _productService.GetAll();

            return list;

        }

        [HttpPost]
        public void Create([FromBody] Product product)
        {
            _productService.Add(product);
            _productService.SaveChanges();
        }

        [HttpGet]
        public Product GetById(int id)
        {
           return  _productService.GetProductById(id);

        }
    }



}