using EShop.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{

    public interface ICustomerRepository : IRepository<Customer>//Truyền kiểu đối tượng vào genegic
    {

    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {


        public CustomerRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }
        /// Các câu truy vấn với csdl thực hiện tại class này 

    }

}
