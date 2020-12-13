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
    public interface IProductClientService
    {
        public void GetAllByCategory();
        public IEnumerable<string> GetTopSaleList();
        public IEnumerable<ProductVersionForSaleDto> GetNewProductList();

        public void GetProductVersionDetail(int id);

    }

    public class ProductClientService : IProductClientService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductVersionRepository _productVerRepository;
        private readonly IMapper _mapper;
        public ProductClientService(IProductRepository productRepository, IProductVersionRepository productVersionRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productVerRepository = productVersionRepository;
            _mapper = mapper;
        }
        public void GetAllByCategory()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductVersionForSaleDto> GetNewProductList()
        {
            var query = _productVerRepository.GetMulti(null, q => q.Include(x => x.Product)
                                 .ThenInclude(y => y.Catalog)
                                  .Include(x => x.Product)
                                  .ThenInclude(y => y.ProductVersions)
                                  .ThenInclude(z => z.ProductVersionImages)
                             .Include(x => x.ProductVersionImages)) ;
            var productsReturn = query.Select(x => _mapper.Map<ProductVersionForSaleDto>(x));
            


            return productsReturn;
        }

        public void GetProductVersionDetail(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetTopSaleList()
        {
            throw new NotImplementedException();
        }
    }
}
