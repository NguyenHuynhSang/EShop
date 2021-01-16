using GHNApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Dtos.Shipping
{
    public class AddressForViewDto
    {
        public int Id { set; get; }

        public string WardCode { set; get; }
        public string WardName { set; get; }
        public string DistrictName { set; get; }
        public string ProvinceName { set; get; }
        public string AddressDetail { set; get; }
        public bool isMain { set; get; }
    }
}
