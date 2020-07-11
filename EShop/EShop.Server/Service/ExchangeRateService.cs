using EShop.Server.Models;
using EShop.Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Service
{
    public interface IExchangeRateService
    {
        void AddOrUpdate(ExchangeRateDongA exchangeRate);
      
        IEnumerable<ExchangeRateDongA> GetAll(string keyword);

        public ExchangeRateDongA GetExchangeRateValueById(int id);

        public ExchangeRateDongA Delete(ExchangeRateDongA exchangeRate);

        void SaveChanges();
   
    }
    public class ExchangeRateService : IExchangeRateService
    {
        IExchangeRateRepository _exchangeRateRepository;


        public ExchangeRateService(IExchangeRateRepository exchangeRateRepository)
        {
            this._exchangeRateRepository = exchangeRateRepository;


        }
        public void AddOrUpdate(ExchangeRateDongA exchangeRate)
        {
            if (_exchangeRateRepository.CheckContains(x=>x.type== exchangeRate.type))
            {
                 _exchangeRateRepository.Update(exchangeRate);
            }


             _exchangeRateRepository.Add(exchangeRate);
        }

       

        public ExchangeRateDongA Delete(ExchangeRateDongA exchangeRate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExchangeRateDongA> GetAll(string keyword)
        {
            return _exchangeRateRepository.GetAll();
        }

        public ExchangeRateDongA GetExchangeRateValueById(int id)
        {
            return _exchangeRateRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _exchangeRateRepository.Commit();
        }

      
    }
}
