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
        public IEnumerable<ProductVersionForSaleDto> GetNewProductList(int numRecord);

        public ProductVersionForSaleDto GetProductVersionDetail(int id);

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

        public IEnumerable<ProductVersionForSaleDto> GetNewProductList(int numRecord)
        {
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true, q => q.Include(x => x.Product)
                                 .ThenInclude(y => y.Catalog)
                                  .Include(x => x.Product)
                                  .ThenInclude(y => y.ProductVersions)
                                  .ThenInclude(z => z.ProductVersionImages)
                             .Include(x => x.ProductVersionImages)) ;
            var productsReturn = query.OrderByDescending(x=>x.Product.CreatedDate).Select(x => _mapper.Map<ProductVersionForSaleDto>(x)).Take(numRecord);      
            return productsReturn;
        }

        public ProductVersionForSaleDto GetProductVersionDetail(int id)
        {
            var query = _productVerRepository.GetMulti(null, q => q.Include(x => x.Product)
                                .ThenInclude(y => y.Catalog)
                                 .Include(x => x.Product)
                                 .ThenInclude(y => y.ProductVersions)
                                 .ThenInclude(z => z.ProductVersionImages)
                            .Include(x => x.ProductVersionImages)).SingleOrDefault(x=>x.Id==id&&x.Product.IsActive==true);
            var productsReturn = _mapper.Map<ProductVersionForSaleDto>(query);

            return productsReturn;
        }

        public IEnumerable<string> GetTopSaleList()
        {
            throw new NotImplementedException();
        }
    }
}
