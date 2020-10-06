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
    public class ExchangeRateController : ApiControllerBase
    {
        private IExchangeRateService _exchangeRateService;// service xử dụng

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            this._exchangeRateService = exchangeRateService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ExchangeRateDongA>> GetAll(string keyword)
        {
            try
            {
                var list = _exchangeRateService.GetAll(keyword);
                return list.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return StatusCode(500);
            }
        
        }




    }
}