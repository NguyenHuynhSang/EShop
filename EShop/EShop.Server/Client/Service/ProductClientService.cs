using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IProductClientService
    {
        public void GetAllByCategory();
        public void GetTopSaleList();
        public void GetNewProductList();

        public void GetProductDetail(int id);

    }

    public class ProductClientService
    {
    }
}
