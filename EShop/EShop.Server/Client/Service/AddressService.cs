using EShop.Server.Data.Repository.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IAddressService
    {
        int GetDistrictByWardCode(string wardCode);
    }
    public class AddressService : IAddressService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        public AddressService(IDistrictRepository districtRepository, IWardRepository wardRepository)
        {
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
        }
        public int GetDistrictByWardCode(string wardCode)
        {
            return _wardRepository.GetSingleByCondition(x=>x.WardCode==wardCode).DistrictId;
        }
    }
}
