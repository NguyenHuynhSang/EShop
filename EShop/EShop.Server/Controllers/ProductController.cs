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
using EShop.Server.Dtos.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using System.IO;
namespace EShop.Server.Controllers
{

    [Route("api/[controller]/[action]")]

    [ApiController]

    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;// service xử dụng
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)

        {
            _mapper = mapper;
            this._productService = productService;
        }


        [HttpGet]
        [EnableCors("ApiCorsPolicy")]
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
                return Ok(list);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                logger.Debug(ex);
                return NotFound();
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
                return Ok(PagedList<ProductForListDto>.ToPagedList(list, page, perPage));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
       
        }

    

        [HttpPost]
        public ActionResult<Product> Create(ProductForInputDto product)
        {
            try
                {
                var productForCreate = _mapper.Map<Product>(product);
                var newProduct = _productService.Add(productForCreate);
                _productService.SaveChanges();
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult Seed()
        {
            try
            {
                _productService.Seed();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound(ex.ToString());

            }
          
        }
      

        [HttpPut]
        public ActionResult<Product> Update(Product product)
        {
            try
            {
                //TEST
                var updatedProduct = _productService.Update(product);
                _productService.SaveChanges();
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
        }



        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            try
            {
                return Ok(_productService.GetProductById(id));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
          
        }

        



        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            try
            {
                var oldEntity = _productService.Delete(id);
                _productService.SaveChanges();
                return oldEntity;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
        }


    }
}