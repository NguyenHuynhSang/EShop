using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Service
{

    public interface IAttributeValueService
    {
        AttributeValue Add(AttributeValue attributeValue);
        IEnumerable<AttributeValue> GetAll(string keyword);

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

        public IEnumerable<AttributeValue> GetAll(string atributeId)
        {
            if (String.IsNullOrEmpty(atributeId))
            {
                return _attributeValueRepository.GetAll();
            }
            else
            {
                return _attributeValueRepository.GetMulti(x => x.AttributeID== Int32.Parse(atributeId));
            }

        }


        public AttributeValue GetAttributeValueById(int id)
        {
            return _attributeValueRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
           
        }
    }
}
