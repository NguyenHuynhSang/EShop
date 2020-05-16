using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{

    public interface IAttributeService
    {
        Model.Models.Attribute Add(Model.Models.Attribute attribute);
        IEnumerable<Model.Models.Attribute> GetAll(string keyword);

        public Model.Models.Attribute GetAttributeById(int id);

        public Model.Models.Attribute Delete(Model.Models.Attribute attribute);

        void SaveChanges();

    }
    public class AttributeService : IAttributeService
    {
        IAttributeRepository _attributeRepository;
        IUnitOfWork _unitOfWork;

        public AttributeService(IAttributeRepository attributeRepository, IUnitOfWork unitOfWork)
        {
            this._attributeRepository = attributeRepository;
            this._unitOfWork = unitOfWork;

        }
        public Model.Models.Attribute Add(Model.Models.Attribute attribute)
        {
            return _attributeRepository.Add(attribute);
        }

        public Model.Models.Attribute Delete(Model.Models.Attribute attribute)
        {
            return _attributeRepository.Delete(attribute);
        }

        public IEnumerable<Model.Models.Attribute> GetAll(string keyword)
        {
  
              return _attributeRepository.GetAll();
          

        }


        public Model.Models.Attribute GetAttributeById(int id)
        {
            return _attributeRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
