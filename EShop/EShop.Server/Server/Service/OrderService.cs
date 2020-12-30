using EShop.Server.Data.Repository;
using EShop.Server.Extension;
using EShop.Server.Models;
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
        IEnumerable<Order> GetAll(Params param);

        public Order GetOrderBId(int id);

        public Order Delete(Order order);

        public Order Update(Order order);

        void SaveChanges();

        


    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public Order Add(Order order)
        {
            return _orderRepository.Add(order);
        }

        public Order Delete(Order order)
        {
            return _orderRepository.Delete(order);
        }

        public IEnumerable<Order> GetAll(Params param)
        {
            return _orderRepository.GetMulti(null,q=>q.Include(x=>x.OrderDetails));
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
