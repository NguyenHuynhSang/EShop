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
    public class ProductCatalogController : ControllerBase
    {
        private ICatalogService _catalogService;// service xử dụng
        public ProductCatalogController(ICatalogService catalogService)
           
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public IEnumerable<CatalogViewModel> GetAll(string keyword)
        {
            var list = _catalogService.GetAll(keyword);

            return list;
        }


        [HttpGet]
        public PagedListWrapper<CatalogViewModel> GetAllPaging(string keyword, string sortBy, string sort = "desc", int pageNumder = 1, int pageSize = 50)
        {
            var list = _catalogService.GetAll(keyword);

            return PagedList<CatalogViewModel>.ToPagedList(list, pageNumder, pageSize);
        }
        [HttpGet]
        public IEnumerable<ProductCatalog> GetParent()
        {
            var list = _catalogService.GetParent();

            return list;
        }

        [HttpGet]
        public IEnumerable<ProductCatalog> GetChild()
        {
            var list = _catalogService.GetChild();

            return list;
        }

        [HttpGet]
        public IEnumerable<CatalogTreeModel> GetTree()
        {
            var list = _catalogService.GetCatalogTree();

            return list;
        }
        [HttpPost]
        public ProductCatalog Create(ProductCatalog catalog)
        {
            var newCatalog = _catalogService.Add(catalog);
            _catalogService.SaveChanges();
            return newCatalog;
        }


       


    }
}