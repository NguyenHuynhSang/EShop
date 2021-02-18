using EShop.Server.Server.Entities;
using EShop.Server.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticalController(IStatisticService statisticService)
        {
            _statisticService = statisticService;


        }
        [HttpGet]
        public ActionResult<IEnumerable<RevenueStatistic>> GetStatistic(DateTime fromDate, DateTime toDate)
        {
            try
            {

                string fdate, tDate;
                if (fromDate!=null)
                    fdate = Convert.ToDateTime(fromDate).ToString("MM/dd/yyyy");
                else
                {
                    fdate = "01/01/2019";
                }
                if (toDate != null)
                    tDate = Convert.ToDateTime(toDate).ToString("MM/dd/yyyy");
                else
                {
                    tDate = DateTime.Now.ToString("MM/dd/yyyy");
                }

                var result = _statisticService.GetStatistic(fdate,tDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
             }

        }
    }
}
