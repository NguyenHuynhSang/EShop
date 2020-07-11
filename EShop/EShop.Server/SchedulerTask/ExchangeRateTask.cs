using AutoMapper;
using EShop.Server.Data;
using EShop.Server.InputModel;
using EShop.Server.Models;
using EShop.Server.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.Server.SchedulerTask
{

    public class ExchangeRateTask : IScheduledTask
    {
        private IMapper _mapper;

        //Consume Scoped Service inside Hosted Service 
        private readonly IServiceScopeFactory _scopeFactory;
        //cannot inject service here, dont know why
        private static string baseUrl = "http://www.dongabank.com.vn/exchange/export";

        public ExchangeRateTask(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;


        }
        //update after 6 hour
        //"* */6 * * *" see interface IScheduledTask for more details
        // chưa load thời gian từ database

        public string Schedule => "* */6 * * *";


        ///chưa xử lý async
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);


            // với Đông Á Bank phải thêm 2 dòng lệnh này:
            request.Headers["User-Agent"] = "Mozilla/5.0 ( compatible ) ";
            request.Headers["Accept"] = "*/*";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            //lấy đối tượng Response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //gọi hàm GetResponseStream() để trả về đối tượng Stream
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string data = reader.ReadToEnd();
            //vì dữ liệu bị bao bọc là ngoặc tròn, ta đổi nó thành rỗng để đúng cấu trúc Json
            data = data.Replace(")", "").Replace("(", "");
            //chuyển dữ liệu Json qua C# class:
            ExchangeRate tigia = (ExchangeRate)JsonConvert.DeserializeObject(data, typeof(ExchangeRate));
            //trả về cho View là 1 danh sách các Item (các dòng Tỉ Giá)


            //careful
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2&tabs=visual-studio#consuming-a-scoped-service-in-a-background-task



            using (var scope = _scopeFactory.CreateScope())
            {

                IExchangeRateService _exchangeRateService = scope.ServiceProvider.GetRequiredService<IExchangeRateService>();
                foreach (var item in tigia.items)
                {
                    var enchangeItem = _mapper.Map<ExchangeRateDongA>(item);
                    _exchangeRateService.AddOrUpdate(enchangeItem);
                    _exchangeRateService.SaveChanges();
                }
            }

        }

        public class Item
        {
            public string type { get; set; }
            public string imageurl { get; set; }
            public string muatienmat { get; set; }
            public string muack { get; set; }
            public string bantienmat { get; set; }
            public string banck { get; set; }
        }
        private class ExchangeRate
        {
            public List<Item> items { get; set; }
        }
    }


}
