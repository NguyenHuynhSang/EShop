using AutoMapper;
using EShop.Server.Client.Dtos;
using EShop.Server.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IProductCatalogClientService
    {
        public IEnumerable<ProductCatalogForMenuDto> GetProductCatalog();
    }
    public class ProductCatalogClientService : IProductCatalogClientService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;
        public ProductCatalogClientService(ICatalogRepository catalogRepository, IMapper mapper) 
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }


        public IEnumerable<ProductCatalogForMenuDto> GetProductCatalog()
        {
            var query = _catalogRepository.GetMulti(x=>x.ParentID==null,q=>q.Include(x=>x.ChildCatalogs));
            var catalogsReturn = query.Select(x => _mapper.Map<ProductCatalogForMenuDto>(x));
            return catalogsReturn;


        }
    }
}
