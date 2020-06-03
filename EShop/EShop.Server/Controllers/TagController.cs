using EShop.Server.Models;
using EShop.Server.Service;
using EShop.WebApp.Infrastructure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace EShop.Server.Controllers
{

    [Route("api/[controller]/[action]")]

    [ApiController]
    public class TagController : ApiBaseController
    {
        private ITagService _TagService;// service xử dụng

        public TagController(ITagService TagService, IErrorService errorService)
            : base(errorService)
        {
            this._TagService = TagService;
        }

        [HttpGet]
        public IEnumerable<Tag> GetAll(string keyword)
        {
            var list = _TagService.GetAll(keyword);

            return list;
        }

        [HttpPost]
        public Tag CreateTag(Tag Tag)
        {
            var newTag = _TagService.Add(Tag);
            _TagService.SaveChanges();
            return newTag;
        }

        [HttpGet]
        public Tag GetById(int id)
        {
            return _TagService.GetTagById(id);
        }

        [HttpDelete]
        public Tag DeleteTag(Tag Tag)
        {
            return _TagService.Delete(Tag);
        }
    }
}