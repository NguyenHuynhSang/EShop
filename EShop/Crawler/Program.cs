using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Crawler
{
    public class Program
    {
        public static string base_curl_path = @"../../../Curl/totoshop.curl";
        public static HttpClient httpClient;
        public static RestClient restClient;
        public static IWebDriver webDriver;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CrawlDataFromApi(base_curl_path);
        }



        public static string CrawlDataFromUrl(string url)
        {

            var htmlReturn = "";
            htmlReturn = httpClient.GetStringAsync(url).Result;
            return htmlReturn;
        }


        public void Crawl(string url)
        {



        }


        public static void CrawlDataFromApi(string path)
        {
            var cUrl = File.OpenText(path).ReadToEnd();

            List<string> headers = new List<string>();
            string url = Regex.Match(cUrl, @"(?<=curl ')(.*?)(?=' \\)", RegexOptions.Singleline).Value;
            var request = new RestRequest();
            var client = new RestClient(url);

            request.Method = Method.GET;
            var headerList = Regex.Matches(cUrl, @"(?<=-H ')(.*?)(?=' \\)", RegexOptions.Singleline).Select(x => x.Value.ToString());
            foreach (var item in headerList)
            {
                var headerName = Regex.Match(item, @"^.*?(?=:)", RegexOptions.Singleline).Value;
                var headerValue = Regex.Match(item, @"(?<=: ).*?(.*)", RegexOptions.Singleline).Value;
                request.AddHeader(headerName, headerValue);

            }
            request.RequestFormat = DataFormat.Xml;



            var response = client.Execute(request);
            var pageSource = response.Content;
            File.WriteAllText(@"../../../OutPut/result.xml", pageSource);

           



        }




    }
}
