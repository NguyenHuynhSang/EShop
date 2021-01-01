using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Client.Dtos;
using EShop.Server.Client.Dtos.Catalog;
using EShop.Server.Client.Dtos.Customer;
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
            public int[] CalalogIds { set; get; }
            public string[] Colors { set; get; }
            public string[] Tags { set; get; }
            public int[] Size { set; get; }

        }
        //[HttpPost]
        //[SwaggerOperationCustom(Summary = "[Trang sản phẩm]Lấy ra tất cả phiên bản sản phẩm có phân trang và filter, filter được wrap lại trong form-data,([swagger]-Bỏ CHECK Send empty value với mấy cái array bên dưới)")]
        //public ActionResult<IEnumerable<ProductVersionForSaleDto>> GetAllProductPagingV2([FromForm] ProductForSaleFilter Filter, string sortBy = "Product.CreatedDate", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        //{
        //    try
        //    {
        //        Params param = new Params();
        //        param.sortBy = sortBy;
        //        param.sort = sort;
        //        param.perPage = perPage;
        //        param.page = page;
        //        var result = _productClientService.GetListProductByConditon(param, Filter);
        //        return Ok(PagedList<ProductVersionForSaleDto>.ToPagedList(result, page, perPage));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}


        [HttpGet]
        [SwaggerOperationCustom(Summary = "[Trang sản phẩm]Lấy ra tất cả phiên bản sản phẩm có phân trang và filter")]
        public ActionResult<IEnumerable<ProductVersionForSaleListDto>> GetAllProductVersionPaging(string keyword,int? MinPrice,int? MaxPrice, [FromQuery] int[] CatalogIds, [FromQuery] string[] Colors, [FromQuery] int[] Sizes, [FromQuery] string[] Tags, string sortBy = "Product.CreatedDate", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        {
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;
                ProductForSaleFilter filter = new ProductForSaleFilter();
                filter.CalalogIds = CatalogIds;
                filter.Colors = Colors;
                filter.Keyword = keyword;
                filter.MinPrice = MinPrice;
                filter.Size = Sizes;
                filter.MaxPrice = MaxPrice;
                filter.Tags = Tags;
                var result = _productClientService.GetListProductByConditon(param, filter);
                return Ok(PagedList<ProductVersionForSaleListDto>.ToPagedList(result, page, perPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public ActionResult<CatalogForFilterDto> GetCatalogListForFilter()
        {
            try
            {
                //TEST
                var result = _productClientService.GetCatalogsForFilter();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult<CatalogForFilterDto> GetSizeListForFilter()
        {
            try
            {
                //TEST
                var result = _productClientService.GetSizesForFilter();
                return Ok(result);
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


        [HttpGet]
        public ActionResult<IEnumerable<ProductVersionRelatedDto>> GetRelatedVersionInSameCatalog(int ProductVersionId)
        {
            try
            {
                //TEST
                var result = _productClientService.GetProductListByVer(ProductVersionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductVersionRelatedDto>> GetRecommendedVersionList(int ProductVersionId)
        {
            try
            {
                //TEST
                var result = _productClientService.GetRecommendProductList(ProductVersionId);
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
