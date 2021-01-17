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
        IEnumerable<Address> AddAddress(Address add);
        Address DeleteAddress(int id);
        Address SetMainAddress(int id);
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
            _addressRepository = addressRepository;
        }

        public IEnumerable<Address> GetAddress(int cusId)
        {
            return _addressRepository.GetMulti(x => x.CustomerId == cusId);
        }

        public int GetDistrictByWardCode(string wardCode)
        {
            return _wardRepository.GetSingleByCondition(x=>x.WardCode==wardCode).DistrictId;
        }

        public IEnumerable<Address> AddAddress(Address add)
        {

            _addressRepository.Add(add);
            if (add.isMain==true)
            {
                var some = _addressRepository.GetMulti(x => x.CustomerId == add.CustomerId ).ToList();
                some.ForEach(a => a.isMain = false);
            }
            _addressRepository.Commit();
            return _addressRepository.GetMulti(x => x.CustomerId == add.CustomerId);
        }

        public Address DeleteAddress(int id)
        {
            var address = _addressRepository.GetSingleById(id);
            var res=_addressRepository.Delete(address);
            _addressRepository.Commit();
            return res;

        }

        public Address SetMainAddress(int id)
        {
            var add = _addressRepository.GetSingleById(id);
            var some = _addressRepository.GetMulti(x => x.CustomerId == add.CustomerId).ToList();
            some.ForEach(a => a.isMain = false);
            add.isMain = true;
            _addressRepository.Commit();
            return add;
        }
    }
}
