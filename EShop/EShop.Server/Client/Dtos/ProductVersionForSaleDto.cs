﻿using EShop.Server.Client.Dtos.Customer;
using EShop.Server.Dtos.Admin.ProductForList;
using EShop.Server.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos
{
    public class ProductVersionForSaleDto
    {
        public string Name { set; get; }

        public ProductForSaleDto Product { set; get; }
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int Quantity { set; get; }
        public int TotalSold { set; get; }
        public string SKU { set; get; }

        public string Barcode { set; get; }


        public string MainImage { set; get; }

        public IEnumerable<RelativeProductVersionDto> RelativeProductVersions { set; get; }

        public IEnumerable<ProductVersionImageDto> ProductVersionImages { set; get; }
    }

    public class RelativeProductVersionDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string MainImage { set; get; }
    }


    public class ProductForSaleDto : ISeoAble
    {
        public int Id { get; set; }


        public int CatalogId { set; get; }

        public string CatalogName { set; get; }
     
        public int AverageRating { set; get; }
        public string Url { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }


        public int Weight { get; set; }

        public decimal OriginalPrice { get; set; }
        public string SEOTitle { get; set; }
        public string SEOUrl { get; set; }
        public string SEODescription { get; set; }
        public DateTime? CreatedDate { get; set; }

        public IEnumerable<ProductCommentDto> ProductComments { set; get; }


    }


    public class ProductCommentDto
    {
        public int Id { set; get; }
    
        public string CustomerName { set; get; }
        public string Comment { set; get; }
        public int Rating { set; get; }
        public bool HasPurchased { set; get; } = false;


        public int Helpful { set; get; }
        public int UnHelpful { set; get; }
        
        public string Title { set; get; }
        public int FromDay { set; get; }

    }
}
