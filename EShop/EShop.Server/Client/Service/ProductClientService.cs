using AutoMapper;
using EShop.Server.Client.Dtos;
using EShop.Server.Client.Dtos.Catalog;
using EShop.Server.Client.Dtos.Customer;
using EShop.Server.Client.Dtos.ProductFilterParam;
using EShop.Server.Extension;
using EShop.Server.Repository;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EShop.Server.Client.Controller.ClientProductController;

namespace EShop.Server.Client.Service
{
    public interface IProductClientService
    {

        public IEnumerable<ProductVersionForSaleDto> GetNewProductList(int numRecord, int? catalogId);

        public IEnumerable<ProductVersionForSaleDto> GetFeatureProductList(int numRecord, int? catalogId);
        public IEnumerable<ProductVersionForSaleDto> GetPromotionProductList(int numRecord, int? catalogId);
        public IEnumerable<ProductVersionRelatedDto> GetProductListByVer(int Verid);
        public IEnumerable<ProductVersionRelatedDto> GetRecommendProductList(int productVersionId);

        public ProductVersionForSaleDto GetProductVersionDetail(int id);
        public ProductVersionForSaleDto GetProductDetail(int id);
        public IEnumerable<ProductVersionForSaleListDto> GetListProductByConditon(Params param, ProductForSaleFilter Filter);

        public bool CheckVersionQuantity(int verId,int quantity);
        public IEnumerable<CatalogForFilterDto> GetCatalogsForFilter();
        public IEnumerable<AttributeForFilterDto> GetSizesForFilter();
    }

    public class ProductClientService : IProductClientService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IAttributeValueRepository _attributeValueRepository;
        private readonly IProductVersionRepository _productVerRepository;
        private readonly IMapper _mapper;
        public ProductClientService(IProductRepository productRepository, IAttributeValueRepository attributeValueRepository, ICatalogRepository catalogRepository, IProductVersionRepository productVersionRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productVerRepository = productVersionRepository;
            _catalogRepository = catalogRepository;
            _attributeValueRepository = attributeValueRepository;
            _mapper = mapper;
        }

        public bool CheckVersionQuantity(int verId, int quantity)
        {
            var record = _productVerRepository.GetSingleById(verId);

            return record.Quantity > quantity ? true : false;
        }

        public IEnumerable<CatalogForFilterDto> GetCatalogsForFilter()
        {
            var query = _catalogRepository.GetMulti(x => x.ParentID != null,
                q=>q.Include(y=>y.Products)
                .ThenInclude(z=>z.ProductVersions));
            var result = query.Select(x => _mapper.Map<CatalogForFilterDto>(x));
            return result.OrderBy(x => x.Name);
        }

        public IEnumerable<ProductVersionForSaleDto> GetFeatureProductList(int numRecord, int? catalogId)
        {
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true && x.Quantity != 0 && x.Product.Catalog.ParentID == catalogId, q => q.Include(x => x.Product)
                                     .ThenInclude(y => y.Catalog)
                                    .Include(x => x.Product)
                                     .ThenInclude(x => x.ProductComments)
                                      .ThenInclude(x => x.Customer)
                                     .Include(x => x.Product)
                                    .ThenInclude(y => y.ProductVersions)
                                    .ThenInclude(z => z.ProductVersionImages)
                                .Include(x => x.ProductVersionImages));
            var productsReturn = query.OrderByDescending(x => x.TotalSold).Select(x => _mapper.Map<ProductVersionForSaleDto>(x)).Take(numRecord);
            return productsReturn;
        }
        static string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        public IEnumerable<ProductVersionForSaleListDto> GetListProductByConditon(Params param, ProductForSaleFilter filter)
        {
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true, q => q.Include(x => x.Product)
                                .ThenInclude(y => y.Catalog)
                                 .Include(x => x.Product)
                                 .ThenInclude(x => x.ProductComments)
                                    .ThenInclude(x => x.Customer)
                                       .Include(x => x.Product)
                                 .ThenInclude(y => y.ProductVersions)
                                 .ThenInclude(z => z.ProductVersionImages)
                            .Include(x => x.ProductVersionImages)
                                .Include(x => x.ProductVersionAttributes));


            query = query.Where(x => String.IsNullOrEmpty(filter.Keyword) ? true : RemoveDiacritics(x.Product.Name.ToLower()).Contains(RemoveDiacritics(filter.Keyword.ToLower())))
                .Where(x => filter.CalalogIds.Count() > 0 ? filter.CalalogIds.Count(y => y == x.Product.CatalogID) > 0 : true)
              .Where(x => filter.Size.Count() > 0 ? filter.Size.Any(y => x.ProductVersionAttributes.Any(z => z.AttributeValueID == y)) : true);
            if (filter.MinPrice != null && (filter.MaxPrice == null || filter.MaxPrice == 0))
            {
                query = query.Where(x => x.PromotionPrice != 0 ? x.PromotionPrice >= filter.MinPrice : x.Price >= filter.MinPrice);
            }
            else if ((filter.MaxPrice != null && filter.MaxPrice != 0) && filter.MinPrice == null)
            {
                query = query.Where(x => x.PromotionPrice != 0 ? x.PromotionPrice <= filter.MaxPrice : x.Price <= filter.MaxPrice);
            }

            else if ((filter.MaxPrice != null && filter.MaxPrice != 0) && filter.MinPrice != null)
            {
                query = query.Where(x => x.PromotionPrice != 0 ? x.PromotionPrice <= filter.MaxPrice && x.PromotionPrice >= filter.MinPrice : x.Price <= filter.MaxPrice && x.Price >= filter.MinPrice);
            }
            var productsReturn = query.Select(x => _mapper.Map<ProductVersionForSaleListDto>(x));
            productsReturn = productsReturn.Where(x => x.Product.AverageRating >= filter.Rating.Value);
            if (filter.CollapsedVersion)
            {
                return productsReturn.DistinctBy(x => x.Product.Id).AsQueryable().OrderByWithDirection(param.sortBy, param.sort);
            }
            else
            {
                return productsReturn.AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);

            }
            
        }

        public IEnumerable<ProductVersionForSaleDto> GetNewProductList(int numRecord, int? catalogId)
        {
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true && x.Quantity != 0 && x.Product.Catalog.ParentID == catalogId, q => q.Include(x => x.Product)
                                    .ThenInclude(y => y.Catalog)
                                   .Include(x => x.Product)
                                    .ThenInclude(x => x.ProductComments)
                                     .ThenInclude(x => x.Customer)
                                    .Include(x => x.Product)
                                   .ThenInclude(y => y.ProductVersions)
                                   .ThenInclude(z => z.ProductVersionImages)
                               .Include(x => x.ProductVersionImages));
            var productsReturn = query.OrderByDescending(x => x.Product.CreatedDate).Select(x => _mapper.Map<ProductVersionForSaleDto>(x)).Take(numRecord);
            return productsReturn;
        }

        public ProductVersionForSaleDto GetProductDetail(int id)
        {
            var query = _productVerRepository.GetMulti(null, q => q.Include(x => x.Product)
                              .ThenInclude(y => y.Catalog)
                               .Include(x => x.Product)
                                .ThenInclude(x => x.ProductComments)
                                 .ThenInclude(x => x.Customer)
                                  .Include(x => x.Product)
                                  .ThenInclude(x => x.ProductComments)
                                .ThenInclude(x => x.ChildComments)
                                 .ThenInclude(x => x.Customer)
                                .Include(x => x.Product)
                               .ThenInclude(y => y.ProductVersions)
                               .ThenInclude(z => z.ProductVersionImages)
                          .Include(x => x.ProductVersionImages)).SingleOrDefault(x => x.Product.Id == id && x.Product.IsActive == true);
            var productsReturn = _mapper.Map<ProductVersionForSaleDto>(query);

            return productsReturn;
        }

        public IEnumerable<ProductVersionRelatedDto> GetProductListByVer(int verId)
        {
            var currentVer = _productVerRepository.GetSingleByCondition(x => x.Id == verId,
                q => q.Include(q => q.Product));
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true && x.Quantity != 0 && x.Product.CatalogID == currentVer.Product.CatalogID && x.Product.Id != currentVer.Product.Id, q => q.Include(x => x.Product)
                                  .ThenInclude(y => y.Catalog)
                                 .Include(x => x.ProductVersionImages));
            var productsReturn = query.OrderByDescending(x => x.Product.CreatedDate).Select(x => _mapper.Map<ProductVersionRelatedDto>(x));
            return productsReturn;
        }

        public ProductVersionForSaleDto GetProductVersionDetail(int id)
        {
            var query = _productVerRepository.GetMulti(null, q => q.Include(x => x.Product)
                                .ThenInclude(y => y.Catalog)
                                 .Include(x => x.Product)
                                  .ThenInclude(x => x.ProductComments)
                                   .ThenInclude(x => x.Customer)
                                    .Include(x => x.Product)
                                    .ThenInclude(x => x.ProductComments)
                                  .ThenInclude(x => x.ChildComments)
                                   .ThenInclude(x => x.Customer)
                                  .Include(x => x.Product)
                                 .ThenInclude(y => y.ProductVersions)
                                 .ThenInclude(z => z.ProductVersionImages)
                            .Include(x => x.ProductVersionImages)).SingleOrDefault(x => x.Id == id && x.Product.IsActive == true);
            var productsReturn = _mapper.Map<ProductVersionForSaleDto>(query);

            return productsReturn;
        }

        public IEnumerable<ProductVersionForSaleDto> GetPromotionProductList(int numRecord, int? catalogId)
        {
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true && x.PromotionPrice > 0 && x.Product.Catalog.ParentID == catalogId, q => q.Include(x => x.Product)
                                     .ThenInclude(y => y.Catalog)
                                 .Include(x => x.Product)
                                  .ThenInclude(x => x.ProductComments)
                                  .ThenInclude(x => x.Customer)
                                  .Include(x => x.Product)
                                 .ThenInclude(y => y.ProductVersions)
                                 .ThenInclude(z => z.ProductVersionImages)
                                .Include(x => x.ProductVersionImages));
            var productsReturn = query.OrderByDescending(x => x.Price - x.PromotionPrice).Select(x => _mapper.Map<ProductVersionForSaleDto>(x)).Take(numRecord);
            return productsReturn;
        }

        public IEnumerable<ProductVersionRelatedDto> GetRecommendProductList(int productVersionId)
        {
            var currentVer = _productVerRepository.GetSingleByCondition(x => x.Id == productVersionId,
               q => q.Include(q => q.Product));
            var query = _productVerRepository.GetMulti(x => x.Product.IsActive == true && x.Quantity != 0 && x.Product.Id != currentVer.Product.Id && x.Product.CatalogID != currentVer.Product.CatalogID,
                q => q.Include(x => x.Product)
                                   .ThenInclude(y => y.Catalog)
                                    .Include(x => x.Product)
                                 .ThenInclude(x => x.ProductComments)
                                    .ThenInclude(x => x.Customer)
                                       .Include(x => x.Product)
                                 .ThenInclude(y => y.ProductVersions)
                                 .ThenInclude(z => z.ProductVersionImages)
                            .Include(x => x.ProductVersionImages));
            var productsReturn = query.OrderByDescending(x => Guid.NewGuid()).Select(x => _mapper.Map<ProductVersionRelatedDto>(x));
            return productsReturn.Take(20);
        }

        public IEnumerable<AttributeForFilterDto> GetSizesForFilter()
        {
            var query = _attributeValueRepository.GetMulti(x => x.AttributeID == 1);
            var result = query.Select(x => _mapper.Map<AttributeForFilterDto>(x));
            return result;
        }


    }
}
