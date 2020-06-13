using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Service;
using EShop.Server.ViewModels;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        INewsService _NewsService;
        public NewsController(INewsService newsService)
        {
            this._NewsService = newsService;
        }

        [HttpGet]
        public IEnumerable<NewsViewmodel> GetNewsForView()
        {
            var list = _NewsService.GetNewsForView();
            return list;
        }
    }
}
