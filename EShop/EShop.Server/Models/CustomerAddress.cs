using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class CustomerAddress
    {
        public int Id { set; get; }
        public int ProvinceId { set; get; }
        public int ProvinceName { set; get; }
        public int DistrictId { set; get; }
        public int DistrictName { set; get; }
        public string WardCode { set; get; }
        public string WardName { set; get; }
        public string AddressDetail { set; get; }
        public string isMain { set; get; }
        




    }
}
