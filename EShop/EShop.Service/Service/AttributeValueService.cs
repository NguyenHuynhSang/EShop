using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
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
        IUnitOfWork _unitOfWork;

        public AttributeValueService(IAttributeValueRepository attributeValueRepository, IUnitOfWork unitOfWork)
        {
            this._attributeValueRepository = attributeValueRepository;
            this._unitOfWork = unitOfWork;

        }
        public AttributeValue Add(AttributeValue attributeValue)
        {
            return _attributeValueRepository.Add(attributeValue);
        }

        public AttributeValue Delete(AttributeValue attributeValue)
        {
            return _attributeValueRepository.Delete(attributeValue);
        }

        public IEnumerable<AttributeValue> GetAll(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return _attributeValueRepository.GetAll();
            }
            else
            {
                return _attributeValueRepository.GetMulti(x => x.Name.Contains(keyword));
            }

        }


        public AttributeValue GetAttributeValueById(int id)
        {
            return _attributeValueRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
