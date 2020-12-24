using System;
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

        [HttpGet]

        public ActionResult<IEnumerable<ProductVersionForSaleDto>> GetAllProductPaging(string sortBy= "Product.CreatedDate", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        {
            try
            {
                Params param = new Params();
       
                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;

                var result = _productClientService.GetListProductByConditon(param);
                return Ok(PagedList<ProductVersionForSaleDto>.ToPagedList(result, page, perPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


   

        [HttpGet]
        public ActionResult<ProductVersionForSaleDto> Detail(int VerId)
        {
            try
            {
                //TEST
                var result = _productClientService.GetProductVersionDetail(VerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }

}
