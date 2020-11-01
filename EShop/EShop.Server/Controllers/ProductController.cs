using EShop.Server.Service;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using EShop.Server.Extension;
using System;
using EShop.Server.Dtos.Admin.ProductForList;
using static EShop.Server.Extension.FilterExtension;

namespace EShop.Server.Controllers
{

    [Route("api/[controller]/[action]")]

    [ApiController]

    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;// service xử dụng

        public ProductController(IProductService productService)

        {
            this._productService = productService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductForListDto>> GetAll(string filterProperty, FilterOperator filterOperator, FilterType filterType,string filterValue, string filterValue1,string sortBy, SortType sort=SortType.desc)
        {
          
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.filterProperty = filterProperty;
                param.filterOperator = filterOperator;
                param.filterValue1 = filterValue1;
                param.filterValue= filterValue;
                param.filterType = filterType;
                var list = _productService.GetAll(param);
                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                logger.Debug(ex);
                return StatusCode(500);
            }
        }
        [HttpGet]
        public ActionResult<PagedListWrapper<ProductForListDto>> GetAllPaging(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort=SortType.desc, decimal? currency=null, string weight="kg", int page = 1, int perPage = 50)
        {
            
            try
            {
                Params param = new Params();
                param.currency = currency;
                param.weight = weight;
                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;
                var list = _productService.GetAll(param);
                return PagedList<ProductForListDto>.ToPagedList(list, page, perPage);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
       
        }

    

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            try
            {
             
                var newProduct = _productService.Add(product);
                _productService.SaveChanges();
                return newProduct;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }


        [HttpPut]
        public ActionResult<Product> UpdateProduct(int id)
        {
            try
            {
                //TEST
               var product= _productService.GetProductById(id); 
                var updatedProduct = _productService.Update(product);
                _productService.SaveChanges();
                return updatedProduct;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }



        [HttpGet]
        public ActionResult<Product> GetById(int id)
        {
            try
            {
                return _productService.GetProductById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
          
        }

        



        [HttpDelete]
        public ActionResult<Product> Delete(Product product)
        {
            try
            {
                var oldEntity = _productService.Delete(product);
                _productService.SaveChanges();
                return oldEntity;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }


    }
}