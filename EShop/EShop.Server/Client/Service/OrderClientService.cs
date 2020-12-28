using AutoMapper;
using EShop.Server.Client.Dtos.Order;
using EShop.Server.Data.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IOrderClientService
    {
        public OrderForCheckOutDto CheckOut(OrderForCheckOutDto order);
        public void SaveChange();
    }
    public class OrderClientService : IOrderClientService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderClientService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

  
      
        public OrderForCheckOutDto CheckOut(OrderForCheckOutDto order)
        {

            Order orderReturn = _mapper.Map<Order>(order);
            orderReturn.CreatedDate = DateTime.Now;
            orderReturn.OrderStatusId = 1;
            _orderRepository.Add(orderReturn);
            return order;

        }

        public void SaveChange()
        {
            _orderRepository.Commit();
        }
    }
}
