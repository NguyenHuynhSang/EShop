using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using EShop.Server.Service;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EShop.Server.Extension;
using AutoMapper;
using static EShop.Server.Extension.FilterExtension;

namespace EShop.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCatalogController : ApiControllerBase
    {
        private readonly ICatalogService _catalogService;// service xử dụng
        private readonly IMapper _mapper;
        public ProductCatalogController(ICatalogService catalogService,IMapper mapper)
           
        {
            _catalogService = catalogService;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CatalogViewModel>> GetAll(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort = SortType.desc)
        {
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.filterProperty = filterProperty;
                param.filterOperator = filterOperator;
                param.filterValue1 = filterValue1;
                param.filterValue = filterValue;
                param.filterType = filterType;
                var list = _catalogService.GetAll(param);
                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }
         
        }


        [HttpGet]
        public ActionResult<PagedListWrapper<CatalogViewModel>> GetAllPaging(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort = SortType.desc, decimal? currency = null, string weight = "kg", int page = 1, int perPage = 50)
        {
            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.filterProperty = filterProperty;
                param.filterOperator = filterOperator;
                param.filterValue1 = filterValue1;
                param.filterValue = filterValue;
                param.filterType = filterType;
                var list = _catalogService.GetAll(param);
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


        [HttpPut]
        public ActionResult<ProductCatalog> Update(ProductCatalog catalog)
        {
            try
            {
                
                var updatedProductCatalog = _catalogService.Update(catalog);
                _catalogService.SaveChanges();
                return updatedProductCatalog;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }


        }


        [HttpPut]
        public ActionResult<ProductCatalog> Active(int ID)
        {
            try
            {
                //TEST
                var updatedProductCatalog = _catalogService.Active(ID);
                _catalogService.SaveChanges();
                return updatedProductCatalog;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }


        }


    }
}