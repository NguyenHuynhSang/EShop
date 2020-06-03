﻿using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Repository
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetByAlias(string alias);
    }
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {


        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Tag> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
