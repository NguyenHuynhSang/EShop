using AutoMapper;
using EShop.Server.Client.Dtos.Order;
using EShop.Server.Data.Repository;
using EShop.Server.Models;
using GHNApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IOrderClientService
    {
        public OrderForCheckOutDto CheckOut(OrderForCheckOutDto order, decimal fee, Address address);
        public void SaveChange();
    }
    public class OrderClientService : IOrderClientService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IGiaoHangNhanhService _giaoHangNhanhService;
        public OrderClientService(IMapper mapper, IOrderRepository orderRepository, IGiaoHangNhanhService giaoHangNhanhService)
        {
            _mapper = mapper;
            _giaoHangNhanhService = giaoHangNhanhService;
            _orderRepository = orderRepository;

        }



        public OrderForCheckOutDto CheckOut(OrderForCheckOutDto order, decimal fee, Address address)
        {

            Order orderReturn = _mapper.Map<Order>(order);

            orderReturn.CreatedDate = DateTime.Now;
            orderReturn.OrderStatusId = 1;
            orderReturn.ShippingFee = (int)fee;
            orderReturn.ShippingDetail = address.AddressDetail + " " + address.Ward.WardName + " " + address.Ward.District.DistrictName + " " + address.Ward.District.Province.ProvinceName;
            _orderRepository.Add(orderReturn);
            return order;

        }

        public void SaveChange()
        {
            _orderRepository.Commit();
        }
    }
}
