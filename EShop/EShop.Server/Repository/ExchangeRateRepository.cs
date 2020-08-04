using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Repository
{

    public interface IExchangeRateRepository : IRepository<ExchangeRateDongA>
    {
       
    }
    public class ExchangeRateRepository : RepositoryBase<ExchangeRateDongA>, IExchangeRateRepository
    {
        public ExchangeRateRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }

      
    }
}
