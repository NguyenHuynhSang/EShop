using EShop.Server.Extension;
using EShop.Server.Models;
using EShop.Server.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EShop.Server.Extension.FilterExtension;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }



        [HttpGet]
 
        public ActionResult<IEnumerable<Order>> GetAll(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort = SortType.desc)
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
                var list = _orderService.GetAll(param);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpGet]
 
        public ActionResult<IEnumerable<Order>> GetAllPaging(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy, SortType sort = SortType.desc, decimal? currency = null, string weight = "kg", int page = 1, int perPage = 50)
        {

            try
            {
                Params param = new Params();
                param.currency = currency;
                param.weight = weight;
                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;
                param.filterProperty = filterProperty;
                param.filterOperator = filterOperator;
                param.filterValue1 = filterValue1;
                param.filterValue = filterValue;
                param.filterType = filterType;
                var list = _orderService.GetAll(param);
                return Ok(PagedList<Order>.ToPagedList(list, page, perPage));
            }
            catch (Exception ex)
            {
   
                return BadRequest(ex);
            }
        }


        [HttpPut]
        public ActionResult<Order> Update(Order order)
        {
            try
            {

                var updatedOrder = _orderService.Update(order);
                _orderService.SaveChanges();
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
    }
}
