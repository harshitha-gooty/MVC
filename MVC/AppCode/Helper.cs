using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MVC.AppCode
{
    public sealed class Helper
    {
        private static readonly Lazy<Helper> _instance = new Lazy<Helper>(() => new Helper());
        public static Helper Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public dynamic PostToWebApi(string apiUrl, dynamic model)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            string postBody = javaScriptSerializer.Serialize(model);
            string statuscode = string.Empty;
            string result = string.Empty;
            var apiResponse = new PostWebApiResponse();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new System.Net.Http.StringContent(postBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                apiResponse.StatusCode = response.IsSuccessStatusCode.ToString();

                apiResponse.ResponseString = response.Content.ReadAsStringAsync().Result;
            }
            return apiResponse.ResponseString;
        }
        public string GetFromWebApi(string apiUrl)
        {
            string result = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }


    }
    public class PostWebApiResponse
    {
        public string StatusCode { get; set; }
        public string ResponseString { get; set; }
    }
}