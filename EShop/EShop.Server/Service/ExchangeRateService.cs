using AutoMapper;
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
        private readonly IMapper _mapper;

        public ExchangeRateService(IExchangeRateRepository exchangeRateRepository,IMapper mapper)
        {
            this._exchangeRateRepository = exchangeRateRepository;
            _mapper = mapper;

        }
        public void AddOrUpdate(ExchangeRateDongA exchangeRate)
        {
            if (_exchangeRateRepository.CheckContains(x=>x.type== exchangeRate.type))
            {
                //ID  must be arrive at the Update Method
                var entity = _exchangeRateRepository.GetSingleByCondition(x=>x.type==exchangeRate.type);
                entity = _mapper.Map<ExchangeRateDongA, ExchangeRateDongA>(exchangeRate, entity);
                _exchangeRateRepository.Update(entity);
            }
            else
            {
                _exchangeRateRepository.Add(exchangeRate);
            }    

           
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
