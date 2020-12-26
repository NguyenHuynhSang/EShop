using EShop.Server.Data.Repository;
using EShop.Server.Models;
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

        void SaveChange();

    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Customer Login(string username, string password)
        {
            var user = _customerRepository.GetSingleByCondition(x => x.Username == username && x.Password == password);

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

        public bool UserExists(string username)
        {
            return this._customerRepository.CheckContains(x => x.Username == username);
        }
    }
}
