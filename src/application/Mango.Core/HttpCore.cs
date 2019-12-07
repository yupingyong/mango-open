using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Mango.Core
{
    public class HttpCore
    {
        /// <summary>
        /// 发起POST同步请求
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="postData"></param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>  
        /// <returns></returns>
        public static ApiResult HttpPost(string apiUrl, string postData = null, string contentType = "application/json")
        {
            postData = postData ?? "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    if (contentType != null)
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    var httpUrl = $"{Configuration.GetItem("ApiServerUrl")}{apiUrl}";
                    HttpResponseMessage response = client.PostAsync(httpUrl, httpContent).Result;
                    return  JsonConvert.DeserializeObject<ApiResult>(response.Content.ReadAsStringAsync().Result);
                }
            }
        }
        /// <summary>
        /// 发起POST同步请求
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="postData"></param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>  
        /// <returns></returns>
        public static ApiResult HttpPut(string apiUrl, string postData = null, string contentType = "application/json")
        {
            postData = postData ?? "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    if (contentType != null)
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    var httpUrl = $"{Configuration.GetItem("ApiServerUrl")}{apiUrl}";
                    HttpResponseMessage response = client.PutAsync(httpUrl, httpContent).Result;
                    return JsonConvert.DeserializeObject<ApiResult>(response.Content.ReadAsStringAsync().Result);
                }
            }
        }
        /// <summary>
        /// 发起GET同步请求
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static ApiResult HttpGet(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var httpUrl = $"{Configuration.GetItem("ApiServerUrl")}{apiUrl}";
                HttpResponseMessage response = client.GetAsync(httpUrl).Result;
                return JsonConvert.DeserializeObject<ApiResult>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
