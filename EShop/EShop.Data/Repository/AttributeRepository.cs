using EShop.Data.DataCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Repository
{

    public interface IAttributeRepository : IRepository<Model.Models.Attribute>
    {
     
    }
    public class AttributeRepository : RepositoryBase<Model.Models.Attribute>, IAttributeRepository
    {
        public AttributeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
