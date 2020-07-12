using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Models;
using EShop.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private IExchangeRateService _exchangeRateService;// service xử dụng

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            this._exchangeRateService = exchangeRateService;
        }
        [HttpGet]
        public IEnumerable<ExchangeRateDongA> GetAll(string keyword)
        {
            var list = _exchangeRateService.GetAll(keyword);
            return list;
        }




    }
}