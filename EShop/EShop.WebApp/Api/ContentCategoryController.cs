using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using EShop.Service.Service;
using EShop.WebApp.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebApp.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentCategoryController : ApiBaseController
    {
        IContentCategoryService _CategoryService;
        public ContentCategoryController(IContentCategoryService categoryService, IErrorService errorService)
            : base(errorService)
        {
            this._CategoryService = categoryService;

        }

        [HttpGet]
        public IEnumerable<ContentCategoryViewmodel> GetContentCategoryForView(string Keyword)
        {
            var list = _CategoryService.GetContentCategoryForView(Keyword);
            return list;

        }

        [HttpGet]
        public ContentCategoryViewmodel GetById(int id)
        {
            return _CategoryService.GetContentCategoryByID(id);
        }

        [HttpPost]
        public ContentCategory CreateContentCategory(ContentCategory category)
        {
            var newCategory = _CategoryService.Add(category);
            _CategoryService.SaveChanges();
            return newCategory;
        }

        [HttpDelete]
        public ContentCategory DeleteCategory(ContentCategory category)
        {
            var delete = _CategoryService.Delete(category);
            _CategoryService.SaveChanges();
            return delete;
        }
        [HttpPut]
        public ContentCategory UpdateCategory(ContentCategory category)
        {
            var update = _CategoryService.Update(category);
            _CategoryService.SaveChanges();
            return update;
        }

    }
}
