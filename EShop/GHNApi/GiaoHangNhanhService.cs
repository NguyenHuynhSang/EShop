﻿using GHNApi.Model;
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
        public IEnumerable<ShippingService> GetSupportedShippingService(int toDistricId);

        public ShippingFee GetShippingFee(string to_ward_code, int to_district_id,int type);

        //public ShippingOrder CreateShippingOrder();
    }
    public class GiaoHangNhanhService : IGiaoHangNhanhService
    {


        public IEnumerable<Province> GetProvince()
        {

            var client = new RestClient(Constain.GET_PROVINCE_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<ProvinceResponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE" + resqContent.Code);
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

        public IEnumerable<ShippingService> GetSupportedShippingService(int toDistrictId)
        {
            var client = new RestClient(Constain.GET_AVALABLE_SHIPPING_SERVICE_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            request.AddParameter("shop_id", Constain.SHOP_ID);
            request.AddParameter("from_district", Constain.SHOP_DEFAULT_DISTRICT_CODE);
            request.AddParameter("to_district", toDistrictId);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<ShippingServiceResponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE" + resqContent.Code);
            return resqContent.Data;
        }

        public ShippingFee GetShippingFee(string to_ward_code, int to_district_id, int type)
        {
            var client = new RestClient(Constain.GET__SHIPPING_FREE_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            request.AddParameter("shop_id", Constain.SHOP_ID);
            request.AddParameter("service_type_id", type);
            request.AddParameter("insurance_value", 0);
            request.AddParameter("to_ward_code", to_ward_code);
            request.AddParameter("to_district_id", to_district_id);
            request.AddParameter("weight", 600);
            request.AddParameter("coupon", null);
            request.AddParameter("length", 30);
            request.AddParameter("width", 30);
            request.AddParameter("height", 10);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<ShippingFreeResponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE" + resqContent.Code);
            return resqContent.Data;
        }
    }
}
