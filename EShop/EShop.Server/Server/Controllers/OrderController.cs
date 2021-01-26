using AutoMapper;
using EShop.Server.Extension;
using EShop.Server.Models;
using EShop.Server.Server.Dtos.Order;
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
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }



        [HttpGet]



        [HttpGet]
        public ActionResult<IEnumerable<OrderForListDto>> GetAllPaging(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy= "Status.id", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        {

            try
            {
                Params param = new Params();
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
                return Ok(PagedList<OrderForListDto>.ToPagedList(list, page, perPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderStatus>>  GetOrderStatus()
        {
            try
            {

                var statuses = _orderService.GetOrderStatus();
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }



        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OrderForDetailDto>> GetOrderDetail(int id)
        {
            try
            {

                var order = _orderService.GetOrderDetail(id);
                var orderReturn = _mapper.Map<OrderForDetailDto>(order);

                return Ok(orderReturn);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }


        [HttpPut]
        public ActionResult<bool> UpdateOrderStatus(int orderId,int StatusId)
        {
            try
            {
                var entity = _orderService.GetOrderBId(orderId);
                entity.OrderStatusId = StatusId;
                var updatedOrder = _orderService.Update(entity);
                _orderService.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }


        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                var oldEntity = _orderService.GetOrderBId(id);
                _orderService.Delete(oldEntity);
                _orderService.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }




    }
}
