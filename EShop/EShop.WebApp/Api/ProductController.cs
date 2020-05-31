using EShop.Model.Models;
using EShop.Service.Service;
using EShop.WebApp.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace EShop.WebApp.Api
{
  
    [Route("api/[controller]/[action]")]
   
    [ApiController]
    public class ProductController : ApiBaseController
    {
        private IProductService _ProductService;// service xử dụng

        public ProductController(IProductService ProductService, IErrorService errorService)
            : base(errorService)
        {
            this._ProductService = ProductService;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll(string keyword)
        {
            var list = _ProductService.GetAll(keyword);

            return list;
        }

        [HttpPost]
        public Product CreateProduct(Product Product)
        {
            var newProduct = _ProductService.Add(Product);
            _ProductService.SaveChanges();
            return newProduct;
        }


         
        //public HttpResponseMessage Create(HttpRequestMessage request, Product Product)
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
        //            var newProduct = _ProductService.Add(Product);
        //            _ProductService.SaveChanges();
        //            msg = request.CreateResponse(HttpStatusCode.Created);
        //        }
        //        return msg;
        //    });


        //}

        [HttpGet]
        public Product GetById(int id)
        {
            return _ProductService.GetProductById(id);
        }

        [HttpDelete]
        public Product DeleteProduct(Product Product)
        {
            return _ProductService.Delete(Product);
        }
    }
}