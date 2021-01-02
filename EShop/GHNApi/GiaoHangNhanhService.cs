using GHNApi.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GHNApi
{
    public interface IGiaoHangNhanhService
    {
        public IEnumerable<Province> GetProvince();
        public IEnumerable<District> GetDistricFromProvinceId(int provinceId);
        public IEnumerable<Ward> GetWardByDistrictId(int Id);
    }
    public class GiaoHangNhanhService: IGiaoHangNhanhService
    {

        
        public IEnumerable<Province> GetProvince()
        {

            var client = new RestClient(Constain.GET_PROVINCE_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<ProvinceResponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE"+resqContent.Code);
            return resqContent.Data;
        }

        public IEnumerable<District> GetDistricFromProvinceId(int provinceId)
        {

            var client = new RestClient(Constain.GET_DISTRIC_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            request.AddParameter("province_id", provinceId);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<DistrictReponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE" + resqContent.Code);
            return resqContent.Data;
        }

        public IEnumerable<Ward> GetWardByDistrictId(int Id)
        {
            var client = new RestClient(Constain.GET_WARD_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            request.AddParameter("district_id", Id);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<WardResponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE" + resqContent.Code);
            return resqContent.Data;
        }
    }
}
