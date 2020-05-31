using EShop.Data.DataCore;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EShop.Data.Repository
{
    public interface IContentTagRepository : IRepository<ContentTag>
    {
        IEnumerable<ContentTag> GetByAlias(string alias);
        public void RemoveAllContentTag(int contentid);
    }
    public class ContentTagRepository : RepositoryBase<ContentTag>, IContentTagRepository
    {


        public ContentTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ContentTag> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllContentTag(int contentid)
        {
            DbContext.ContentTags.RemoveRange(DbContext.ContentTags.Where(x => x.ContentID == contentid));
            DbContext.SaveChanges();
        }
    }
}
