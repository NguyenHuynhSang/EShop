using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Client.Dtos;
using EShop.Server.Client.Service;
using EShop.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/home/[action]")]
    [ApiController]
    public class ClientHomeController : ControllerBase
    {
        private readonly ISlideClientService _slideClientService;
        private readonly IMenuClientService _menuClientService;
        private readonly IProductClientService _productClientService;
        private readonly IProductCatalogClientService _productCatalogClientService;
        public ClientHomeController(ISlideClientService slideClientService, IMenuClientService menuClientService,IProductClientService productClientService, IProductCatalogClientService productCatalogClientService)
        {
            _slideClientService = slideClientService;
            _menuClientService = menuClientService;
            _productClientService = productClientService;
            _productCatalogClientService = productCatalogClientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Slide>> GetAllSlide()
        {
            try
            {
                //TEST
                var slides = _slideClientService.GetAllSlide();
                return Ok(slides);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]

        public ActionResult<IEnumerable<ProductCatalogForMenuDto>> GetProductCatalogMenu()
        {
            try
            {
                //TEST
                var catalogs = _productCatalogClientService.GetProductCatalog();
                return Ok(catalogs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



        [HttpGet]
        public ActionResult<IEnumerable<Menu>> GetMainMenu()
        {
            try
            {
                //TEST
                var menu = _menuClientService.GetMenu();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductForSaleDto>> GetNewProductList(int count=20)
        {
            try
            {
                //TEST
                var result = _productClientService.GetNewProductList(20);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductForSaleDto>> GetTopSaleProductList(int count = 20)
        {
            try
            {
                //TEST
                var result = _productClientService.GetFeatureProductList(20);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductForSaleDto>> GetTopPromotionProductList(int count = 20)
        {
            try
            {
                //TEST
                var result = _productClientService.GetPromotionProductList(20);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}