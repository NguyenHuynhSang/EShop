using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Service
{
    public interface ICatalogService
    {
        ProductCatalog Add(ProductCatalog productAttribute);
        ProductCatalog Update(ProductCatalog productAttribute);
        ProductCatalog Active(int ID);

        IEnumerable<CatalogViewModel> GetAll(string keyword);

        IEnumerable<CatalogTreeModel> GetCatalogTree();

        public ProductCatalog GetCatalogById(int id);

        public ProductCatalog Delete(ProductCatalog catalog);

        void SaveChanges();

        IEnumerable<ProductCatalog> GetParent();
        IEnumerable<ProductCatalog> GetChild();

    }
    public class CatalogService : ICatalogService
    {
        ICatalogRepository _catalogRepository;

       

        public CatalogService(ICatalogRepository catalogRepository)
        {
            this._catalogRepository = catalogRepository;
           

        }

        public ProductCatalog Active(int ID)
        {
            var catalog = _catalogRepository.GetSingleById(ID);
            catalog.IsActive = catalog.IsActive == null ? false : catalog.IsActive;
            catalog.IsActive = !catalog.IsActive;
            _catalogRepository.Update(catalog);
            return catalog;
        }

        public ProductCatalog Add(ProductCatalog catalog)
        {
            return _catalogRepository.Add(catalog);
        }

        public ProductCatalog Delete(ProductCatalog catalog)
        {
            return _catalogRepository.Delete(catalog);
        }

        public IEnumerable<CatalogViewModel> GetAll(string keyword)
        {
            return _catalogRepository.GetAllCatalogForView();
        }

        public ProductCatalog GetCatalogById(int id)
        {
            return _catalogRepository.GetSingleById(id);
        }

        public IEnumerable<CatalogTreeModel> GetCatalogTree()
        {
            return _catalogRepository.GetTreeCatalog();
        }

        public IEnumerable<ProductCatalog> GetChild()
        {
            return _catalogRepository.GetChildCatalog();
        }

        public IEnumerable<ProductCatalog> GetParent()
        {
            return _catalogRepository.GetParentCatalog();
        }

        public void SaveChanges()
        {

            _catalogRepository.Commit();
        }

        public ProductCatalog Update(ProductCatalog catalog)
        {
       
            return  _catalogRepository.Update(catalog);
        }
    }
}
