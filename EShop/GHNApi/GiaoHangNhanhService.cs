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
    }
    public class GiaoHangNhanhService: IGiaoHangNhanhService
    {

        
        public IEnumerable<Province> GetProvince()
        {

            var client = new RestClient(Constain.GetProvince_URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", Constain.Token);
            IRestResponse response = client.Execute(request);
            var resqContent = JsonConvert.DeserializeObject<ProvinceResponse>(response.Content);
            if (resqContent.Code != 200) throw new Exception("API LOI CODE"+resqContent.Code);
            return resqContent.Data;
        }


    }
}
