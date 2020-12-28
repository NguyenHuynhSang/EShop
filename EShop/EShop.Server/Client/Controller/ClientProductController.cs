﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Client.Dtos;
using EShop.Server.Client.Service;
using EShop.Server.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/product/[action]")]
    [ApiController]
    public class ClientProductController : ControllerBase
    {
        private readonly IProductClientService _productClientService;
        public ClientProductController(IProductClientService productClientService)
        {
            _productClientService = productClientService;
        }




        public class ProductForSaleFilter
        {
            public string Keyword { set; get; }
            public int? MinPrice { set; get; }
            public int? MaxPrice { set; get; }
            public List<int> CalalogIds { set; get; } = new List<int>();
            public string[] Colors { set; get; }
            public string[] Tags { set; get; }
            public string[] Sizes { set; get; }

        }
        [HttpPost]
        [SwaggerOperationCustom(Summary = "[Trang sản phẩm]Lấy ra tất cả phiên bản sản phẩm có phân trang và filter, filter được wrap lại trong form-data,([swagger]-Bỏ CHECK Send empty value với mấy cái array bên dưới)")]
        public ActionResult<IEnumerable<ProductVersionForSaleDto>> GetAllProductVersionPagingV2([FromForm] ProductForSaleFilter Filter, string sortBy = "Product.CreatedDate", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        {
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;
                var result = _productClientService.GetListProductByConditon(param, Filter);
                return Ok(PagedList<ProductVersionForSaleDto>.ToPagedList(result, page, perPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        [SwaggerOperationCustom(Summary = "[Trang sản phẩm]Lấy ra tất cả phiên bản sản phẩm có phân trang và filter, filter được wrap lại trong form-data,([swagger]-Bỏ CHECK Send empty value với mấy cái array bên dưới)")]
        public ActionResult<IEnumerable<ProductVersionForSaleDto>> GetAllProductVersionPaging(string Keyword, int? MinPrice, int? MaxPrice, List<int> CalalogIds, string[] Tags, string[] Sizes, string[] Colors, string sortBy = "Product.CreatedDate", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        {
            try
            {
                Params param = new Params();
                ProductForSaleFilter filter = new ProductForSaleFilter();
                filter.CalalogIds = CalalogIds;
                filter.Colors = Colors;
                filter.Keyword = Keyword;
                filter.MaxPrice = MaxPrice;
                filter.MinPrice = MinPrice;
                filter.Sizes = Sizes;
                filter.Tags = Tags;

                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;
                var result = _productClientService.GetListProductByConditon(param, filter);
                return Ok(PagedList<ProductVersionForSaleDto>.ToPagedList(result, page, perPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [SwaggerOperationCustom(Summary = "[Trang chi tiết sản phẩm]Lấy ra chi tiết phiên bản sản phẩm dựa vào ProductVersionId truyền vào")]
        [HttpGet("{ProductVersionId}")]

        public ActionResult<ProductVersionForSaleDto> VersionDetail(int ProductVersionId)
        {
            try
            {
                //TEST
                var result = _productClientService.GetProductVersionDetail(ProductVersionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[HttpGet("{ProductId}")]

        //public ActionResult<ProductVersionForSaleDto> ProductDetail(int ProductId)
        //{
        //    try
        //    {
        //        //TEST
        //        var result = _productClientService.GetProductVersionDetail(ProductId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}



    }

}
