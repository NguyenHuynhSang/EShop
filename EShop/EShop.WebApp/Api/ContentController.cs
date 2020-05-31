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
    public class ContentController : ApiBaseController
    {
        IContentService _ContentService;
        public ContentController(IContentService contentService, IErrorService errorService)
            : base(errorService)
        {
            this._ContentService = contentService;

        }

        [HttpGet]
        public IEnumerable<ContentViewmodel> GetContentForView(string Keyword)
        {
            var list = _ContentService.GetContentForView(Keyword);
            return list;

        }

        [HttpGet]
        public ContentViewmodel GetById(int id)
        {
            return _ContentService.GetContentByID(id);
        }

        [HttpPost]
        public Content CreateContent(Content content)
        {
            var newContent = _ContentService.Add(content);
            _ContentService.SaveChanges();
            return newContent;
        }

        [HttpDelete]
        public Content DeleteContent(Content content)
        {
            var delete = _ContentService.Delete(content);
            _ContentService.SaveChanges();
            return delete;
        }
        [HttpPut]
        public Content UpdateContent(Content content)
        {
            var update = _ContentService.Update(content);
            _ContentService.SaveChanges();
            return update;
        }

    }
}
