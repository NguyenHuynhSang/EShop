﻿using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Server.Extension;
using System.Linq;

namespace EShop.Server.Service
{

    public interface IAttributeService
    {
        EShop.Server.Models.Attribute Add(EShop.Server.Models.Attribute attribute);
        IEnumerable<EShop.Server.Models.Attribute> GetAll(Params param);
        public EShop.Server.Models.Attribute GetAttributeById(int id);

        public EShop.Server.Models.Attribute Delete(EShop.Server.Models.Attribute attribute);


        public Models.Attribute Update(Models.Attribute attribute);

        void SaveChanges();

    }
    public class AttributeService : IAttributeService
    {
        IAttributeRepository _attributeRepository;
       

        public AttributeService(IAttributeRepository attributeRepository)
        {
            this._attributeRepository = attributeRepository;
           

        }
        public EShop.Server.Models.Attribute Add(EShop.Server.Models.Attribute attribute)
        {
            return _attributeRepository.Add(attribute);
        }

        public EShop.Server.Models.Attribute Delete(EShop.Server.Models.Attribute attribute)
        {
            return _attributeRepository.Delete(attribute);
        }

        public IEnumerable<EShop.Server.Models.Attribute> GetAll(Params param)
        {

            var query = _attributeRepository.GetAll();
            if (!String.IsNullOrEmpty(param.filterProperty))
            {
                query = query.AsQueryable().WhereTo(param);
            }
            return query.AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort); ;
          

        }


        public EShop.Server.Models.Attribute GetAttributeById(int id)
        {
            return _attributeRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
             _attributeRepository.Commit();
        }

        public Models.Attribute Update(Models.Attribute attribute)
        {
            return _attributeRepository.Update(attribute);
        }
    }
}
