using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using EShop.Server.Service;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCatalogController : ApiControllerBase
    {
        private ICatalogService _catalogService;// service xử dụng
        public ProductCatalogController(ICatalogService catalogService)
           
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CatalogViewModel>> GetAll(string keyword)
        {
            try
            {
                var list = _catalogService.GetAll(keyword);
                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
         
        }


        [HttpGet]
        public ActionResult<PagedListWrapper<CatalogViewModel>> GetAllPaging(string keyword, string sortBy, string sort = "desc", int page = 1, int perPage = 50)
        {
            try
            {
                var list = _catalogService.GetAll(keyword);

                return PagedList<CatalogViewModel>.ToPagedList(list, page, perPage);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
          
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductCatalog>> GetParent()
        {
          
            try
            {
                var list = _catalogService.GetParent();

                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductCatalog>> GetChild()
        {
            try
            {
                var list = _catalogService.GetChild();
                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
           
        }

        [HttpGet]
        public ActionResult<IEnumerable<CatalogTreeModel>> GetTree()
        {
        
            try
            {
                var list = _catalogService.GetCatalogTree();
                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public ActionResult<ProductCatalog> Create(ProductCatalog catalog)
        {
         
            try
            {
                var newCatalog = _catalogService.Add(catalog);
                _catalogService.SaveChanges();
                return newCatalog;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        }


    }
}