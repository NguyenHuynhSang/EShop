using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Server.Extension;
using System.Linq;

namespace EShop.Server.Service
{

    public interface IAttributeValueService
    {
        AttributeValue Add(AttributeValue attributeValue);
        IEnumerable<AttributeValue> GetAll(Params param);

        public AttributeValue GetAttributeValueById(int id);

        public AttributeValue Delete(AttributeValue attributeValue);

        void SaveChanges();

    }
    public class AttributeValueService : IAttributeValueService
    {
        IAttributeValueRepository _attributeValueRepository;


        public AttributeValueService(IAttributeValueRepository attributeValueRepository)
        {
            this._attributeValueRepository = attributeValueRepository;


        }
        public AttributeValue Add(AttributeValue attributeValue)
        {
            return _attributeValueRepository.Add(attributeValue);
        }

        public AttributeValue Delete(AttributeValue attributeValue)
        {
            return _attributeValueRepository.Delete(attributeValue);
        }

        public IEnumerable<AttributeValue> GetAll(Params param)
        {

            var query = _attributeValueRepository.GetAll();
            if (!String.IsNullOrEmpty(param.filterProperty))
            {
                query = query.AsQueryable().WhereTo(param);
            }
             query = query.AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
            return query;


        }


        public AttributeValue GetAttributeValueById(int id)
        {
            return _attributeValueRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _attributeValueRepository.Commit();
        }
    }
}
