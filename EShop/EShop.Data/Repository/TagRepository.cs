using EShop.Data.DataCore;
using EShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EShop.Data.Repository
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetByAlias(string alias);

        public bool CheckTag(string ID);
    }
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public bool CheckTag(string ID)
        {
            return DbContext.Tags.Count(x => x.TagID == ID) > 0;
        }

        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Tag> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
