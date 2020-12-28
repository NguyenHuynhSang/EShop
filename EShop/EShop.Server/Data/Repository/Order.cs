using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {


    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {


        public OrderRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }


    }
}
