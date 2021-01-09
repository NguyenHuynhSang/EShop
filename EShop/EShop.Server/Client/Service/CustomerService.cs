using AutoMapper;
using EShop.Server.Client.Dtos.Customer;
using EShop.Server.Data.Repository;
using EShop.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface ICustomerService
    {

        Customer Register(Customer user);
        Customer Login(string username, string password);
        bool UserExists(string username);

        Customer UpdateInfor(CustomerForUpdateDto input);
        void SaveChange();

    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public Customer Login(string username, string password)
        {
            var user = _customerRepository.GetSingleByCondition(x => x.Username == username && x.Password == password,
                q=>q.Include(x=>x.Addresses)
                .ThenInclude(x=>x.Ward)
                .ThenInclude(x => x.District)
                .ThenInclude(x => x.Province));

            if (user == null) return null;
            return user;
        }

        public Customer Register(Customer user)
        {
            return _customerRepository.Add(user);
        }

        public void SaveChange()
        {
            _customerRepository.Commit();
        }

        public Customer UpdateInfor(CustomerForUpdateDto input)
        {
            var old = _customerRepository.GetSingleById(input.Id);
            var update= _mapper.Map<CustomerForUpdateDto,Customer>(input,old);
            return _customerRepository.Update(update);
        }

        public bool UserExists(string username)
        {
            return this._customerRepository.CheckContains(x => x.Username == username);
        }
    }
}
