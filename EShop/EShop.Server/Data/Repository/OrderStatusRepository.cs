using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{
    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {


    }

    public class OrderStatusRepository : RepositoryBase<OrderStatus>, IOrderStatusRepository
    {


        public OrderStatusRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }


    }
}
