using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using EShop.Service.Service;
using EShop.WebApp.Infrastructure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebApp.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ApiBaseController
    {
        private ICatalogService _catalogService;// service xử dụng
        public CatalogController(ICatalogService catalogService, IErrorService errorService)
           : base(errorService)
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
        public IEnumerable<Catalog> GetParent()
        {
            var list = _catalogService.GetParent();

            return list;
        }

        [HttpGet]
        public IEnumerable<Catalog> GetChild()
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
        public Catalog Create(Catalog catalog)
        {
            var newCatalog = _catalogService.Add(catalog);
            _catalogService.SaveChanges();
            return newCatalog;
        }


       


    }
}