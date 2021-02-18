using EShop.Server.Data;
using EShop.Server.Data.Repository;
using EShop.Server.Server.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Snickler.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Service
{
    public interface IStatisticService
    {
        public IEnumerable<RevenueStatistic> GetStatistic(string fromDate, string toDate);

    }
    public class StatisticService : IStatisticService
    {
        public EShopDbContext _dbContext;
        public StatisticService(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<RevenueStatistic> GetStatistic(string fromDate, string toDate)
        {
            var parameter = new object[]{
                new SqlParameter("@fromDate",fromDate),
                   new SqlParameter("@toDate",toDate),
            };
            
          //  var list = _dbContext.Set<RevenueStatisticViewModelDateType>().FromSqlRaw("GetRevenueStatistic @fromDate,@toDate", parameter).ToList();



            IList<RevenueStatisticViewModelDateType> list = null;
            _dbContext.LoadStoredProc("dbo.GetRevenueStatistic")
                .WithSqlParam("fromDate", fromDate)
                 .WithSqlParam("toDate", toDate)
                .ExecuteStoredProc((handler) =>
                {
                    list = handler.ReadToList<RevenueStatisticViewModelDateType>();
                });

            List<RevenueStatistic> listReturn = new List<RevenueStatistic>();

            foreach (var item in list)
            {
                RevenueStatistic val = new RevenueStatistic();
                val.Benefis = item.Benefis;
                val.Revenunes = item.Revenunes;
                val.Date = item.Date.ToString("dd/MMM/yyyy");

                listReturn.Add(val);
            }



            return listReturn;

        }
    }
}
