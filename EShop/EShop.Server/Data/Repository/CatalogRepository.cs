using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EShop.Server.ViewModels;

namespace EShop.Server.Repository
{

    public interface ICatalogRepository : IRepository<ProductCatalog>
    {
        IEnumerable<ProductCatalog> GetChildCatalog();
        IEnumerable<ProductCatalog> GetParentCatalog();

        IEnumerable<CatalogViewModel> GetAllCatalogForView();

        IEnumerable<CatalogTreeModel> GetTreeCatalog();

    }
    public class CatalogRepository : RepositoryBase<ProductCatalog>, ICatalogRepository
    {


        public CatalogRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<CatalogViewModel> GetAllCatalogForView()
        {
            var parentList = DbContext.ProductCatalogs.Where(x => x.ParentID == null);
            var querry = from c in DbContext.ProductCatalogs
                         join p in DbContext.ProductCatalogs
                         on c.ParentID equals p.Id
                         into ps
                         from p in ps.DefaultIfEmpty()
                         select new CatalogViewModel
                         {
                             Catalog = c,
                             ParentName = p.Id == null ? "(GỐC)" : p.Name
                         };

            return querry.ToList();
        }

        public IEnumerable<ProductCatalog> GetChildCatalog()
        {
            var querry = DbContext.ProductCatalogs.Where(x => x.ParentID != null);
            return querry;

        }

        public IEnumerable<ProductCatalog> GetParentCatalog()
        {
            var querry = DbContext.ProductCatalogs.Where(x => x.ParentID == null);
            return querry;
        }

        public IEnumerable<CatalogTreeModel> GetTreeCatalog()
        {
            var querry = from p in DbContext.ProductCatalogs.Where(x => x.ParentID == null)
                         select new CatalogTreeModel
                         {
                             Parent = p,
                             Childs = DbContext.ProductCatalogs.Where(x => x.ParentID == p.Id)
                         };
            return querry.OrderBy(x => x.Parent.CreatedDate).ToList();
        }
    }
}
