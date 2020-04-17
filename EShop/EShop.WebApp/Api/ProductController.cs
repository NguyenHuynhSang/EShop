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
        private IProductService _productService;

        public ProductController(IProductService productService, IErrorService errorService)
            : base(errorService)
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
    }
}