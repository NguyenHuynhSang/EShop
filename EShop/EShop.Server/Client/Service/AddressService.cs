using EShop.Server.Client.Dtos.Shipping;
using EShop.Server.Data.Repository;
using EShop.Server.Data.Repository.Address;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IAddressService
    {
        int GetDistrictByWardCode(string wardCode);
        IEnumerable<Address> GetAddress(int cusId);
        IEnumerable<Address> AddAddress(int cusId,Address add);
    }
    public class AddressService : IAddressService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IAddressRepository _addressRepository;
        public AddressService(IDistrictRepository districtRepository, IWardRepository wardRepository, IAddressRepository addressRepository)
        {
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
        }

        public IEnumerable<Address> GetAddress(int cusId)
        {
            return _addressRepository.GetMulti(x => x.CustomerId == cusId);
        }

        public int GetDistrictByWardCode(string wardCode)
        {
            return _wardRepository.GetSingleByCondition(x=>x.WardCode==wardCode).DistrictId;
        }

        public IEnumerable<Address> AddAddress(int cusId, Address add)
        {
            _addressRepository.Add(add);
            _addressRepository.Commit();
            return _addressRepository.GetMulti(x => x.CustomerId == cusId);
        }


    }
}
