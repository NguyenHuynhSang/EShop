using EShop.Data.DataCore;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Repository
{

    public interface ICatalogRepository : IRepository<Catalog>
    {
        IEnumerable<Catalog> GetChildCatalog();
        IEnumerable<Catalog> GetParentCatalog();

        IEnumerable<CatalogViewModel> GetAllCatalogForView();

        IEnumerable<CatalogTreeModel> GetTreeCatalog();

    }
    public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
    {


        public CatalogRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<CatalogViewModel> GetAllCatalogForView()
        {
            var parentList = DbContext.Catalogs.Where(x => x.ParentID == null);
            var querry = from c in DbContext.Catalogs
                         join p in DbContext.Catalogs
                         on c.ParentID equals p.ID
                         into ps
                         from p in ps.DefaultIfEmpty()
                         select new CatalogViewModel
                         {
                             Catalog = c,
                             ParentName = p.ID == null ? "(GỐC)" : p.Name
                         };

            return querry.ToList();
        }

        public IEnumerable<Catalog> GetChildCatalog()
        {
            var querry = DbContext.Catalogs.Where(x => x.ParentID != null);
            return querry;

        }

        public IEnumerable<Catalog> GetParentCatalog()
        {
            var querry = DbContext.Catalogs.Where(x => x.ParentID == null);
            return querry;
        }

        public IEnumerable<CatalogTreeModel> GetTreeCatalog()
        {
            var querry = from p in DbContext.Catalogs.Where(x => x.ParentID == null)
                         select new CatalogTreeModel
                         {
                             Parent = p,
                             Childs = DbContext.Catalogs.Where(x => x.ParentID == p.ID)
                         };
            return querry.OrderBy(x => x.Parent.CreatedDate).ToList();
        }
    }
}
