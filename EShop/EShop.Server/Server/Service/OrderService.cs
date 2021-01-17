using AutoMapper;
using EShop.Server.Data.Repository;
using EShop.Server.Extension;
using EShop.Server.Models;
using EShop.Server.Server.Dtos.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Service
{
    public interface IOrderService
    {
        Order Add(Order order);
        IEnumerable<OrderForListDto> GetAll(Params param);

        public Order GetOrderBId(int id);

        public Order Delete(Order order);

        public Order Update(Order order);

        void SaveChanges();

        


    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }


        public Order Add(Order order)
        {
            return _orderRepository.Add(order);
        }

        public Order Delete(Order order)
        {
            return _orderRepository.Delete(order);
        }

        public IEnumerable<OrderForListDto> GetAll(Params param)
        {
            var query = _orderRepository.GetMulti(null, q => q.Include(x => x.OrderDetails)
                                                            .Include(x=>x.Customer)
                                                            .Include(x=>x.OrderDetails)
                                                            .Include(x=>x.Status));
            var result=query.Select(x => _mapper.Map<OrderForListDto>(x));
            return result;
        }

        public Order GetOrderBId(int id)
        {
            return _orderRepository.GetSingleByCondition(x=>x.Id==id, q => q.Include(x => x.OrderDetails));
        }

        public void SaveChanges()
        {
            _orderRepository.Commit();
        }

        public Order Update(Order order)
        {
            return _orderRepository.Update(order);
        }
    }
}
