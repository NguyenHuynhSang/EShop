﻿using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface ICatalogService
    {
        Catalog Add(Catalog productAttribute);
        void Update(Catalog productAttribute);

        IEnumerable<CatalogViewModel> GetAll(string keyword);

        public Catalog GetCatalogById(int id);

        public Catalog Delete(Catalog catalog);

        void SaveChanges();

        IEnumerable<Catalog> GetParent();
        IEnumerable<Catalog> GetChild();

    }
    public class CatalogService : ICatalogService
    {
        ICatalogRepository _catalogRepository;
        IUnitOfWork _unitOfWork;

        public CatalogService(ICatalogRepository catalogRepository, IUnitOfWork unitOfWork)
        {
            this._catalogRepository = catalogRepository;
            this._unitOfWork = unitOfWork;

        }
        public Catalog Add(Catalog catalog)
        {
            return _catalogRepository.Add(catalog);
        }

        public Catalog Delete(Catalog catalog)
        {
            return _catalogRepository.Delete(catalog);
        }

        public IEnumerable<CatalogViewModel> GetAll(string keyword)
        {
            return _catalogRepository.GetAllCatalogForView();
        }

        public Catalog GetCatalogById(int id)
        {
            return _catalogRepository.GetSingleById(id);
        }

        public IEnumerable<Catalog> GetChild()
        {
            return _catalogRepository.GetChildCatalog();
        }

        public IEnumerable<Catalog> GetParent()
        {
            return _catalogRepository.GetParentCatalog();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Catalog catalog)
        {
            _catalogRepository.Update(catalog);
        }
    }
}
