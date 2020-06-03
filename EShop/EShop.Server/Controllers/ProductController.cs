using EShop.Server.Service;
using EShop.WebApp.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Linq;
using EShop.Server.InputModel;
using EShop.Server.FilterModel;
using Newtonsoft.Json;
using EShop.Server.Models;

namespace EShop.Server.Controllers
{
  
    [Route("api/[controller]/[action]")]
   
    [ApiController]
    public class ProductController : ApiBaseController
    {
        private IProductService _productService;// service xử dụng

        public ProductController(IProductService productService, IErrorService errorService)
            : base(errorService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll(string filterJson)
        {
            ProductFilterModel filterModel = null;
            if (!string.IsNullOrEmpty(filterJson))
            {
                filterModel = JsonConvert.DeserializeObject<ProductFilterModel>(filterJson);
            }

            var list = _productService.GetAll(filterModel) ;

            return list;
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


        //public HttpResponseMessage Create(HttpRequestMessage request, Product product)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage msg = null;
        //        if (ModelState.IsValid)
        //        {
        //            var message = string.Join(" | ", ModelState.Values
        //               .SelectMany(v => v.Errors)
        //               .Select(e => e.ErrorMessage));
        //            request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
        //        }
        //        else
        //        {
        //            var newProduct = _productService.Add(product);
        //            _productService.SaveChanges();
        //            msg = request.CreateResponse(HttpStatusCode.Created);
        //        }
        //        return msg;
        //    });


        //}

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