using EShop.Server.Data;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EShop.Server.FilterModel;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EShop.Server.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);

       
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        private IMapper _mapper;
        public ProductRepository(EShopDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this._mapper = mapper;
        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
